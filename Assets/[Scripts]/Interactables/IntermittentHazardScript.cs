// IntermittentHazardScript.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class IntermittentHazardScript : MonoBehaviour
{
    private GameObject player;
    public float hazardDamage = 1;
    public float damageTimeCounter = 0;
    public float timeBetweenDamage = 1.5f;

    public float currentTime;
    public ParticleSystem particle;
    public Collider collider;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        particle = gameObject.GetComponent<ParticleSystem>();
        collider = gameObject.GetComponent<Collider>();
    }

    void Update()
    {
        if (currentTime <= 11.0f)
        {
            currentTime += Time.deltaTime;
        }

        TurnOnAndOff();
    }

    public void TurnOnAndOff()
    {
        if (damageTimeCounter <= timeBetweenDamage)
        {
            damageTimeCounter += Time.deltaTime;
        }

        switch (currentTime)
        {
            case >= 10.0f:
                currentTime = 0;
                break;
            case >= 5.0f:
                particle.enableEmission = false;
                collider.enabled = false;
                break;
            case >= 0:
                particle.enableEmission = true;
                collider.enabled = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && damageTimeCounter >= timeBetweenDamage)
        {
            player.GetComponent<PlayerManager>().Hit(hazardDamage);
            damageTimeCounter = 0;
        }
    }
}
