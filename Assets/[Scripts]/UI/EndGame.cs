// EndGame.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            this.GetComponent<GameManagerAPI>().SetFinishedGame();
        }
    }
}
