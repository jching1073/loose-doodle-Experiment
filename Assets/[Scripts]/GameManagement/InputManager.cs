// InputManager.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using UnityEngine;

[DisallowMultipleComponent]
public class InputManager : MonoBehaviour
{
    /// <summary>
    ///   <para>Singleton instance for easy referencing</para>
    ///   <para>It is very important that there is only one instance of this in the whole scene.</para>
    /// </summary>
    public static InputManager Instance { get; private set; }

    [Header("Control Keys")]
    public string jump = "start";
    public string right = "d";
    public string left = "a";
    public string forward = "w";
    public string backward = "s";
    public string shoot = "mouse 0";
    public string select = "e";
    public string settings = "esc";
    public string power1 = "1";
    public string power2 = "2";
    public string power3 = "3";
    public string power4 = "4";

    public void SetJump(string key) { jump = key; }

    public void SetRight(string key) { right = key.ToLower(); }

    public void SetLeft(string key) { left = key.ToLower(); }

    public void SetForward(string key) { forward = key.ToLower(); }

    public void SetBackward(string key) { backward = key.ToLower(); }

    public void SetShoot(string key) { shoot = key.ToLower(); }

    public void SetSelect(string key) { select = key.ToLower(); }

    public void SetSettings(string key) { settings = key.ToLower(); }

    public void SetPower1(string key) { power1 = key.ToLower(); }

    public void SetPower2(string key) { power2 = key.ToLower(); }

    public void SetPower3(string key) { power3 = key.ToLower(); }

    public void SetPower4(string key) { power4 = key.ToLower(); }

    void Awake()
    {
        // check if there's already an instance, and destroy self if there is
        if (Instance)
        {
            Debug.LogError($"Only one InputManager should exist in the scene - game object `{name}`");
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
