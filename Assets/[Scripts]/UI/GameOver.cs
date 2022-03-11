// GameOver.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using UnityEngine;

public class GameOver : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            GameManager.Instance.SetGameOver();
        }

    }
}
