// Blocker.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using System.Collections;
using System.IO;
using UnityEngine;

public class Blocker : MonoBehaviour, ISaveLoad
{
    public Animator anim;
    public SoundFile eraseSound;

    private AudioSource audioSource;

    // Prevent multiple activation from going in and out of the trigger
    private bool markedForDelete;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!markedForDelete && other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerManager>().hasEraser == true)
            {
                Disappear(true);
            }
        }
    }

    private void Disappear(bool playEffects)
    {
        if (markedForDelete) { return; }

        markedForDelete = true;
        anim.SetBool("IsOpen", true);

        if (playEffects)
        {
            eraseSound.PlayWithSource(audioSource);
            UIManager.Instance.UsingEraser(true);
            StartCoroutine(Timer());
            Invoke(nameof(RemoveColliders), 1.2f);
        }
        else
        {
            RemoveColliders();
        }

        IEnumerator Timer()
        {
            yield return new WaitForSeconds(0.2f);
            UIManager.Instance.UsingEraser(false);
            // UIManager.Instance.SetEraser(false);
        }
    }

    private void RemoveColliders()
    {
        foreach (Collider collider in GetComponents<Collider>())
        {
            collider.enabled = false;
        }
    }

    public void Save(TextWriter writer)
    {
        writer.WriteLine(markedForDelete ? "Blocker erased" : "Blocker not erased");
    }

    public void Load(TextReader reader)
    {
        string line = reader.ReadLine();
        if (line == "Blocker erased")
        {
            Disappear(false);
        }
    }
}
