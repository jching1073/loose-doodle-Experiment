// PlatformController.cs
// Owned by Garabatos Inc.
// Created by: Zhengliang Ding (301222388)

using UnityEngine;

public class PlatformController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }


}
