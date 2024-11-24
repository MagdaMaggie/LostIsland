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

    private PlayerStats playerStats;

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
        //DontDestroyOnLoad(gameObject);
        playerStats = PlayerStats.singleton;
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
        }
    }

    public void UpdateUI()
    {
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
