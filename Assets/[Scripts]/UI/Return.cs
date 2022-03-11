// Return.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using UnityEngine;

public class Return : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.settings))
        {
            GameManager.Instance.ResumeGame();
        }
    }
}
