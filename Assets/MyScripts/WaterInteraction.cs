using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    private bool isPlayerInRange = false; // Flag, um zu prüfen, ob der Spieler in der Nähe ist
    public int waterAmount = 25; // Menge des Wassers, die die Water Bar erhöht

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Überprüft, ob der Spieler den Collider betritt
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Überprüft, ob der Spieler den Collider verlässt
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Prüft, ob der Spieler 'E' drückt
        {
            // Ruft die Methode im PlayerStats-Skript auf, um Wasser zu trinken
            PlayerStats playerStats = FindObjectOfType<PlayerStats>(); // Sucht das PlayerStats-Skript im Spiel
            if (playerStats != null)
            {
                playerStats.DrinkWater(waterAmount); // Erhöht die Water Bar um waterAmount
                Debug.Log("You drank water and restored " + waterAmount + "%!");
            }
        }
    }
}
