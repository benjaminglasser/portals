using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{

    public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;


    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping)
        {
            // calculating if the player is in front or behind of the portal
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.right, portalToPlayer);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
            {
                // Teleport the player!

                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciever.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit (Collider other)

    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}