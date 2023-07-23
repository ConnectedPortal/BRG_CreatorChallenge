using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Movement Values")]
    private Vector3 moveInput;
    [SerializeField] private float speed;
    /*
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxAcceleration;
    */

    private void FixedUpdate()
    {
        PlayerSteering();
    }

    private void PlayerSteering()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        Vector3 movement = transform.TransformDirection(moveInput) * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }



}
