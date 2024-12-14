using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            PlayerHP playerHealth = other.GetComponent<PlayerHP>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(3); // Reduce the player's HP by 1
            }

            PooledObject pooledObject = this.GetComponent<PooledObject>();
            if (pooledObject != null)
                pooledObject.ReturnObject(); // Return this object to the pool
        }

    }
}