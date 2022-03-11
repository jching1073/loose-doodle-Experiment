// MiniMapCamera.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public GameObject player;
    public LayerMask firstFloorOnly;
    public LayerMask plusSecondFloor;
    public LayerMask all;

    private new Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Check the y position of the player and displays that floor
    void Update()
    {
        camera.cullingMask = player.transform.position.y switch
        {
            < 10f => firstFloorOnly,
            > 10f and < 18f => plusSecondFloor,
            > 18f => all,
            _ => camera.cullingMask,
        };
    }
}
