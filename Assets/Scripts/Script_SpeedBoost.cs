using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SpeedBoost : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private Script_PlayerMovement scriptPlayerMovement;

    [Header("Values")]
    private float speedMemory;

    private void Start()
    {
        speedMemory = scriptPlayerMovement.speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scriptPlayerMovement.speed += speedMemory;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scriptPlayerMovement.speed -= speedMemory;
        }
    }
}
