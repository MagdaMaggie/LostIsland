using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : MonoBehaviour
{
    public GameObject berryBushWithBerries; 
    public GameObject berryBushEmpty;      

    public PlayerStats playerStats;

    private bool playerInRange = false;

    void Start()
    {
        berryBushEmpty.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectBerries();
            playerStats.AddBerryToInventory(); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerStats = other.GetComponent<PlayerStats>();
            Debug.Log("Drücke 'E', um Beeren zu sammeln.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerStats = null;
        }
    }

    void CollectBerries()
    {
        berryBushWithBerries.SetActive(false);
        berryBushEmpty.SetActive(true);

        Debug.Log("Beeren gesammelt!");
    }
}
