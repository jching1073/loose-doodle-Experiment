// TriggerSpawn.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    public GameObject spawner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            spawner.GetComponent<Spawners>().isSpawning = true;
        }
    }
}
