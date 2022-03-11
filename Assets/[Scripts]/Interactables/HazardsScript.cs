// HazardsScript.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class HazardsScript : MonoBehaviour
{
    private GameObject player;
    public float hazardDamage = 1;
    public float damageTimeCounter = 0;
    public float timeBetweenDamage = 1.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (damageTimeCounter <= timeBetweenDamage)
        {
            damageTimeCounter += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) { return; }

        Debug.Log("Player Touching");
        if (damageTimeCounter >= timeBetweenDamage)
        {
            player.GetComponent<PlayerManager>().Hit(hazardDamage);
            damageTimeCounter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) { return; }

        Debug.Log("Player Touching");
        if (damageTimeCounter >= timeBetweenDamage)
        {
            player.GetComponent<PlayerManager>().Hit(hazardDamage);
            damageTimeCounter = 0;
        }
    }
}
