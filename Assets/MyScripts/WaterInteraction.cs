using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    private bool isPlayerInRange = false; 
    public int waterAmount = 25; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            PlayerStats playerStats = FindObjectOfType<PlayerStats>(); 
            if (playerStats != null)
            {
                playerStats.DrinkWater(waterAmount); 
                Debug.Log("You drank water and restored " + waterAmount + "%!");
            }
        }
    }
}
