// HealthManager.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using UnityEngine;
using UnityEngine.Events;

/// <summary>
///   <para></para>
/// </summary>
[DisallowMultipleComponent]
public class HealthManager : MonoBehaviour
{
    /// <summary>
    ///   <para>Singleton instance for easy referencing</para>
    ///   <para>It is very important that there is only one instance of this in the whole scene.</para>
    /// </summary>
    public static HealthManager Instance { get; private set; }

    public float health;

    public UnityEvent<float> onHealthChanged;

    public void SetHealth(float h)
    {
        if (health > h)
        {
            GameManager.Instance.PlayerHurt();
        }

        health = h;
        onHealthChanged.Invoke(health);
    }

    void Awake()
    {
        // check if there's already an instance, and destroy self if there is
        if (Instance)
        {
            Debug.LogError($"Only one HealthManager should exist in the scene - game object `{name}`");
            Destroy(this);
            return;
        }

        // set the singleton instance to use
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
