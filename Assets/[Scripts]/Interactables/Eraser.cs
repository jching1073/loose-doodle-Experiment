// Eraser.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class Eraser : MonoBehaviour, IItemPickUp
{
    public void OnItemPickUp(PlayerManager player)
    {
        player.PickUpEraser();
    }
}
