// FlashLight.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class FlashLight : MonoBehaviour, IItemPickUp
{
    public void OnItemPickUp(PlayerManager player)
    {
        player.GetComponentInChildren<WeaponManager>().PickUpFlashLight();
    }
}
