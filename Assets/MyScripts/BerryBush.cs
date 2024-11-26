using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : MonoBehaviour
{
    public GameObject berryBushWithBerries; // Referenz zum Strauch mit Beeren
    public GameObject berryBushEmpty;       // Referenz zum leeren Strauch

    public PlayerStats playerStats; // Reference to the PlayerStats script

    private bool playerInRange = false;

    void Start()
    {
        // Der Strauch ohne Beeren soll zun�chst unsichtbar sein
        berryBushEmpty.SetActive(false);
    }

    void Update()
    {
        // Pr�fen, ob der Spieler in der N�he ist und die "E"-Taste dr�ckt
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectBerries();
            playerStats.AddBerryToInventory(); // Add berry to inventory
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Wenn der Spieler in den Interaktionsbereich eintritt
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerStats = other.GetComponent<PlayerStats>();
            Debug.Log("Dr�cke 'E', um Beeren zu sammeln.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Wenn der Spieler den Interaktionsbereich verl�sst
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerStats = null;
        }
    }

    void CollectBerries()
    {
        // Strauch mit Beeren deaktivieren und leeren Strauch aktivieren
        berryBushWithBerries.SetActive(false);
        berryBushEmpty.SetActive(true);

        Debug.Log("Beeren gesammelt!");
    }
}
