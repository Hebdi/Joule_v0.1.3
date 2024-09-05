using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public string playerTag = "Player"; // Tag used to identify the player
    public float rotationSpeed = 5f; // Speed at which the object rotates
    public float detectionRange = 10f; // Range within which the object starts rotating towards the player
    public float rotationOffset = 90f; // Adjust this value if the object is turned away from the player

    private Transform playerTransform;
    private Quaternion initialRotation; // Store the initial rotation

    void Start()
    {
        // Store the initial rotation
        initialRotation = transform.rotation;

        // Find the player object by tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Calculate the direction to the player
                Vector3 directionToPlayer = playerTransform.position - transform.position;
                directionToPlayer.y = 0; // Keep the object level by ignoring the Y axis

                // Calculate the target rotation to face the player
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

                // Apply rotation offset to correct the orientation
                targetRotation *= Quaternion.Euler(0, rotationOffset, 0);

                // Smoothly rotate towards the player
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                // Smoothly rotate back to the initial rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
