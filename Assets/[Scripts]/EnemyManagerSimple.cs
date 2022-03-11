// EnemyManagerSimple.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManagerSimple : MonoBehaviour
{
    private GameObject player;
    public Animator enemyAnimator;
    public float damage = 1;
    public float enemyHealth;

    public bool playerInReach;
    public float attackDelayTimer;

    public Slider slider;
    public GameObject spawner;

    //What happens when enemy is attacked (called in WeaponManager)
    public void HitEnemy(float damage)
    {
        enemyHealth -= damage;
        slider.value = enemyHealth;
        if(enemyHealth <= 0)
        {
            spawner.GetComponent<Spawners>().DefeatedEnemy();
            enemyAnimator.SetTrigger("IsDead");
            Destroy(gameObject, 5f);
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<EnemyManagerSimple>());
            Destroy(GetComponent<CapsuleCollider>());
        }
    }

    void Start()
    {
        //On Start find player object
        player = GameObject.FindGameObjectWithTag("Player");
        slider.maxValue = enemyHealth;
        slider.value = enemyHealth;
        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    void Update()
    {
        slider.transform.LookAt(player.transform);
        GetComponent<NavMeshAgent>().destination = player.transform.position;
        enemyAnimator.SetBool("IsWalking", GetComponent<NavMeshAgent>().velocity.magnitude > 0.5f);
    }

    //Triggers playerInReach When enemy and player collides
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInReach = true;

        }
    }

    //Delayed attack on enemy nth seconds between attack and initial attack
    private void OnCollisionStay(Collision collision)
    {
        if (!playerInReach) { return; }

        attackDelayTimer += Time.deltaTime;

        if (attackDelayTimer >= 0)
        {
            enemyAnimator.SetTrigger("IsAttacking");
        }

        if (attackDelayTimer >= 5.5f)
        {
            player.GetComponent<PlayerManager>().Hit(damage);
            attackDelayTimer = 0;
        }
    }

    //When player and enemy are not touching anymore reset delay and playerInReach
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInReach = false;
            attackDelayTimer = 0;
        }
    }

}
