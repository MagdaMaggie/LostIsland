using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    private bool isPlayerInRange = false; // Flag, um zu pr�fen, ob der Spieler in der N�he ist
    public int waterAmount = 25; // Menge des Wassers, die die Water Bar erh�ht

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �berpr�ft, ob der Spieler den Collider betritt
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �berpr�ft, ob der Spieler den Collider verl�sst
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Pr�ft, ob der Spieler 'E' dr�ckt
        {
            // Ruft die Methode im PlayerStats-Skript auf, um Wasser zu trinken
            PlayerStats playerStats = FindObjectOfType<PlayerStats>(); // Sucht das PlayerStats-Skript im Spiel
            if (playerStats != null)
            {
                playerStats.DrinkWater(waterAmount); // Erh�ht die Water Bar um waterAmount
                Debug.Log("You drank water and restored " + waterAmount + "%!");
            }
        }
    }
}
