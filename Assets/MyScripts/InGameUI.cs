using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI singleton;

    public Slider healthBar;
    public Slider foodBar;
    public Slider waterBar;
    public Image[] inventorySlots; 
    public Sprite emptySlotSprite;
    public Sprite berrySprite;
    public Sprite woodSprite;
    public Sprite berrySoupSprite;

    public PlayerStats playerStats;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {        
        if (healthBar == null || foodBar == null || waterBar == null)
        {
            Debug.LogError("One or more sliders are not assigned!");
        }

        if (emptySlotSprite == null || berrySprite == null || woodSprite == null || berrySoupSprite == null)
        {
            Debug.LogError("One or more sprites are not assigned!");
        }

        UpdateUI();
        InitializeInventorySlots();
    }

    public void InitializeInventorySlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] != null)
            {
                inventorySlots[i].sprite = emptySlotSprite;
                inventorySlots[i].enabled = true;
            }

            if (inventorySlots[i] == null)
            {
                Debug.LogError($"Inventory slot at index {i} is null!");
                continue;
            }
        }
    }

    public void UpdateUI()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats reference is missing in InGameUI!");
            return;
        }

        if (playerStats != null)
        {
            healthBar.value = playerStats.currentHealth;
            foodBar.value = playerStats.currentFood;
            waterBar.value = playerStats.currentWater;
        }
    }

    public void UpdateInventoryUI(int index, string item)
    {
        if (item != null)
        {
            if (item =="Berry")
            {
                inventorySlots[index].sprite = berrySprite;
            }
            else if (item =="Wood")
            {
                inventorySlots[index].sprite = woodSprite;
            }
            else if (item == "BerrySoup")
            {
                inventorySlots[index].sprite = berrySoupSprite;
            }

            inventorySlots[index].enabled = true;
        }
        else
        {
            inventorySlots[index].sprite = emptySlotSprite;
            inventorySlots[index].enabled = true;
        }

    }
}
