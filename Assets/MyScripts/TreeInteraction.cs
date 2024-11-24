using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range of tree");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player left the trigger
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left the range of tree");
        }
    }

    private void Update()
    {
        // Check if the player is in range and "E" key is pressed
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Call method to add wood to inventory
            PlayerStats.singleton.AddWoodToInventory();
            
            // Destroy the tree after adding wood
            Destroy(gameObject); // Destroy the tree
            Debug.Log("Tree removed");
        }
    }
}
