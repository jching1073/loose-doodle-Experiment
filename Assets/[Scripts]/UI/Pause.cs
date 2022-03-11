// Pause.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using UnityEngine;

public class Pause : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.settings))
        {
            GameManager.Instance.PauseGame();
        }
    }
}
