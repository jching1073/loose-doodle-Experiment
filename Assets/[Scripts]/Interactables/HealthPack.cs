// HealthPack.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public Canvas canvas;

    //Pick Up health packs
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().HealthPackPickUp();
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        canvas.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}
