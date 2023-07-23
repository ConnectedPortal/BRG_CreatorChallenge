using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;


    [Header("Movement Values")]
    private Vector3 moveInput;
    private Vector3 moveForce;

    [SerializeField] public float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float steerStrength;

    [Header("Capsize Values")]
    [SerializeField] private bool isCapsized;
    [SerializeField] private float rotationTime;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        CapsizeDetect();
        CapsizeFlip();
    }

    private void FixedUpdate()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        PlayerVelocity();
        PlayerSteering();
    }

    private void PlayerVelocity()
    {
        moveForce += transform.forward * speed * moveInput.z * Time.deltaTime;
        transform.position += moveForce * Time.deltaTime;

        moveForce *= drag;
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);
    }

    private void PlayerSteering()
    {
        float steerInput = moveInput.x;
        transform.Rotate(Vector3.up * steerInput * steerStrength * Time.deltaTime);
    }

    private void CapsizeDetect()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Water"))
            {
                Invoke("CapsizedBool", 2f);
            }
        }
    }

    private void CapsizedBool()
    {
        isCapsized = true;
    }

    private void CapsizeFlip()
    {
        if (isCapsized)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                /*
                Vector3 oldRotation += transform.rotation;
                Vector3 newRotation = new Vector3(transform.rotation.x, 0, transform.rotation.z);

                transform.rotation = Vector3.Lerp(transform.rotation, newRotation, Time.deltaTime * 3f);
                */
                Vector3 newRotation = new Vector3(transform.rotation.x, 0, transform.rotation.z);

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z), rotationTime * rotationSpeed);
                rotationTime = rotationTime + Time.deltaTime;
                isCapsized = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(-Vector3.up) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
