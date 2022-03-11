// PlayerManager.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerManager : MonoBehaviour, ISaveLoad
{
    public float health = 8;
    public int healthPacks = 3;
    public bool hasKey;
    public bool hasEraser;
    [Header("Sound")]
    public SoundFile hurtSound;
    public SoundFile healSound;
    private AudioSource soundPlayer;

    /// <summary>
    ///   <para>Property for the health value that automatically notifies relevant objects</para>
    /// </summary>
    public float Health
    {
        get => health;
        set
        {
            health = value;
            HealthManager.Instance.SetHealth(health);

            if (health <= 0)
                GameManager.Instance.SetGameOver();
        }
    }

    //What happens when player gets hit (called by enemy scripts)
    public void Hit(float damage)
    {
        Health = Mathf.Max(Health - damage, 0.0f);
        hurtSound.PlayWithSource(soundPlayer);
    }

    // Used to heal character when health packs are available
    public void UseHealthPack()
    {
        if (healthPacks > 0)
        {
            if (health < 8)
            {
                Health += 1;
                healthPacks--;
                UIManager.Instance.SetFirstAidOn(true, healthPacks);
                healSound.PlayWithSource(soundPlayer);
                StartCoroutine(Timer());
            }
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.2f);
        UIManager.Instance.usingHealthPack(false);
    }

    public void PickUpKey()
    {
        if (hasKey == false)
        {
            hasKey = true;
            UIManager.Instance.SetKey(true);
        }
    }

    public void PickUpEraser()
    {
        if (hasEraser == false)
        {
            hasEraser = true;
            UIManager.Instance.SetEraser(true);
        }
    }

    // Used when picking up heath packs
    public void HealthPackPickUp()
    {
        healthPacks++;
        UIManager.Instance.SetFirstAidOn(true, healthPacks);
    }

    private void HealthPackSet(int amount)
    {
        healthPacks = Mathf.Max(amount, 0);
        UIManager.Instance.SetFirstAidOn(true, healthPacks);
    }

    void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (healthPacks > 0)
        {
            UIManager.Instance.SetFirstAidOn(true, healthPacks);
        }

        Health = health; // apply current value to the manager objects
    }

    void Update()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;

        if (Input.GetKeyDown(InputManager.Instance.power1))
        {
            UIManager.Instance.usingHealthPack(true);
            UseHealthPack();
        }
    }

    public void Save(TextWriter writer)
    {
        // So tempting to do below
        // writer.WriteLine(JsonUtility.ToJson(new PlayerData(this)));

        writer.WriteLine("Player");

        // Transform information
        Vector3 pos = transform.position;
        writer.WriteLine($"- Position: {pos.x} {pos.y} {pos.z}");

        // Health information
        writer.WriteLine($"- Health: {health}");

        // Items information
        writer.WriteLine($"- Health Packs: {healthPacks}");
        writer.WriteLine($"- Eraser: {hasEraser}");
        writer.WriteLine($"- Key: {hasKey}");
    }

    public void Load(TextReader reader)
    {
        // TODO implement load for the player
        // Use PickUpBlahBlah methods
        // For health packs, use HealthPackSet
    }
}
