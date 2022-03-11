// EnemyAI.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public Animator enemyAnimator;

    public float enemyHealth = 3;
    public float enemyDamage = 1;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public float distanceBetween;
    public float sightRange;
    public float attackRange;

    public Slider slider;

    //Line of Sight Variables
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool isPlayerVisible;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        slider.maxValue = enemyHealth;
        slider.value = enemyHealth;
        StartCoroutine(POVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.LookAt(player.transform);
        distanceBetween = Vector3.Distance(player.transform.position, this.transform.position);
        if(distanceBetween > sightRange)
        {
            Idle();
        }
        else if(distanceBetween <= sightRange && distanceBetween >= attackRange && isPlayerVisible == true)
        {
            Chase();
        }
        else if(distanceBetween < attackRange && isPlayerVisible == true)
        {
            Attack();
        }
    }

    //What the enemy does when Idle
    public void Idle()
    {
        enemyAnimator.SetBool("IsAttacking", false);
        enemyAnimator.SetBool("IsChasing", false);
    }

    //What the enemy does when in Chase
    public void Chase()
    {
        enemyAnimator.SetBool("IsAttacking", false);
        GetComponent<NavMeshAgent>().destination = player.transform.position;
        enemyAnimator.SetBool("IsChasing", GetComponent<NavMeshAgent>().velocity.magnitude > 0f);
    }

    //What the enemy does when an Attack
    private void Attack()
    {
        if (!alreadyAttacked)
        {
            enemyAnimator.SetBool("IsAttacking", true);
            player.GetComponent<PlayerManager>().Hit(enemyDamage);
            alreadyAttacked = true;
            //Delay reset of attack
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    //Resets attack
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //What happens when player attacks enemy (called in WeaponsManager)
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        slider.value = enemyHealth;

        if (enemyHealth <= 0)
        {
            enemyAnimator.SetTrigger("IsDead");
            DestroyEnemy();
        }
    }

    //What happens when enemy dies
    private void DestroyEnemy()
    {
        Destroy(gameObject, 3);
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<EnemyAI>());
        Destroy(GetComponent<CapsuleCollider>());
    }

    //Using co-routine to improve performance 1 second delay for scan
    private IEnumerator POVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1.0f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeCheck.Length == 0)
        {
            isPlayerVisible = false;
            return;
        }

        Transform target = rangeCheck[0].transform;
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, directionToTarget) >= angle / 2)
        {
            isPlayerVisible = false;
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
        {
            isPlayerVisible = false;
            return;
        }

        isPlayerVisible = true;
        Debug.Log("I Can See You");
    }
}
