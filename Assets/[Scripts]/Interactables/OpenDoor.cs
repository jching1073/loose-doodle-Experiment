// OpenDoor.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using System.Collections;
using System.IO;
using UnityEngine;

public class OpenDoor : MonoBehaviour, ISaveLoad
{
    [Header("Open Sound")]
    public SoundFile openSound;

    private Animator anim;
    private AudioSource audioSource;

    // Prevent multiple activation from going in and out of the trigger
    private bool isOpen;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen && other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerManager>().hasKey == true)
            {
                Open(true);
            }
        }
    }

    private void Open(bool playEffects)
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;
        anim.SetBool("IsOpen", true);

        if (playEffects)
        {
            openSound.PlayWithSource(audioSource);
            UIManager.Instance.UsingKey(true);
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            yield return new WaitForSeconds(0.2f);
            UIManager.Instance.UsingKey(false);
            // UIManager.Instance.SetKey(false);
        }
    }

    public void Save(TextWriter writer)
    {
        writer.WriteLine(isOpen ? "Door open" : "Door closed");
    }

    public void Load(TextReader reader)
    {
        string line = reader.ReadLine();
        if (line == "Door open")
        {
            Open(false);
        }
    }
}
