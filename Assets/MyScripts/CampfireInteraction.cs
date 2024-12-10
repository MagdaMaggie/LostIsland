using UnityEngine;

public class CampfireInteraction : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private PlayerStats playerStats;

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is near the campfire
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerStats = other.GetComponent<PlayerStats>();
            Debug.Log("Player is near the campfire.");
            Debug.Log($"isPlayerInRange set to: {isPlayerInRange}");
            Debug.Log($"OnTriggerEnter called. PlayerStats is null: {playerStats == null}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Player leaves the campfire range
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerStats = null;            
            Debug.Log("Player left campfire range.");
            Debug.Log($"isPlayerInRange set to: {isPlayerInRange}");
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed...");
            if (playerStats != null)
            {
                bool success = playerStats.ConvertBerryToSoup();
                if (success)
                {
                    Debug.Log("Berry converted to Berry Soup!");
                }
                else
                {
                    Debug.Log("No berry in inventory to convert.");
                }
            }
        }
    }
}
