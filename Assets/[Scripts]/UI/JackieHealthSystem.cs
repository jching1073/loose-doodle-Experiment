// JackieHealthSystem.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   <para>Manage which sprite to show for current health</para>
/// </summary>
public class JackieHealthSystem : MonoBehaviour
{
    public Sprite[] jackieHS;

    private Image img;
    private int spriteIndex;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    public void OnPlayerHealthChanged(float health)
    {
        spriteIndex = Convert.ToInt32(health);
        if(spriteIndex > 0)
        {
            img.sprite = jackieHS[spriteIndex-1];
        }        
        Debug.Log("Index: "+spriteIndex);
    }
}
