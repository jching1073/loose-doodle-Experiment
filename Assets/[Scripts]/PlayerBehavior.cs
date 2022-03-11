// PlayerBehavior.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Sound Effects")]
    public SoundFile jumpSound;
    public SoundFile landSound; // TODO implement landing detection
    public SoundFile pickUpSound;

    private CharacterController controller;
    private AudioSource soundPlayer;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        soundPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if(isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = GetInputAxis(InputManager.Instance.right, InputManager.Instance.left);
        float z = GetInputAxis(InputManager.Instance.forward, InputManager.Instance.backward);

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * maxSpeed * Time.deltaTime);

        if(Input.GetKeyDown(InputManager.Instance.jump) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            jumpSound.PlayWithSource(soundPlayer);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    public void PickUpSound()
    {
        pickUpSound.PlayWithSource(soundPlayer);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            pickUpSound.PlayWithSource(soundPlayer);
        }
       
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    private float GetInputAxis(string positive, string negative)
    {
        return Convert.ToSingle(Input.GetKey(positive)) - Convert.ToSingle(Input.GetKey(negative));
    }
}
