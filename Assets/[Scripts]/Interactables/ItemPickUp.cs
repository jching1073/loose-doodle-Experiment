// ItemPickUp.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

using System.IO;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class ItemPickUp : MonoBehaviour, ISaveLoad
{
    [SerializeField]
    private string itemName;

    private bool pickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (!pickedUp && other.CompareTag("Player"))
        {
            transform.SendMessage("OnItemPickUp",
                other.GetComponent<PlayerManager>(),
                SendMessageOptions.RequireReceiver);
            Disappear();
        }
    }

    private void Disappear()
    {
        if (pickedUp) { return; }

        pickedUp = true;

        // Disable any colliders on the object
        foreach (Collider collider in GetComponents<Collider>())
        {
            collider.enabled = false;
        }

        // Inactivate all children objects
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Save(TextWriter writer)
    {
        writer.WriteLine(pickedUp ? $"{itemName} nom" : $"{itemName} intact");
    }

    public void Load(TextReader reader)
    {
        string line = reader.ReadLine();
        if (line == $"{itemName} nom")
        {
            Disappear();
        }
    }
}
