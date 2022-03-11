// SaveBtn.cs
// Owned by Garabatos Inc.
// Created by: Zhengliang Ding (301222388)

using UnityEngine;

public class SaveBtn : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem saveSystem = GameObject.Find("LevelManager").GetComponent<SaveSystem>();
        saveSystem.SaveGame();
    }
}
