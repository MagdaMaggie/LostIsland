using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxFood = 100;
    public int maxWater = 100;

    public int currentHealth;
    public int currentFood;
    public int currentWater;

    public float foodDecayRate = 1f;
    public float waterDecayRate = 2f;

    public GameObject berryPrefab;
    public GameObject woodPrefab;
    public GameObject CampfirePrefab;    

    public InGameUI inGameUI;

    public int woodCount = 0;

    private bool isHealthDecreasing = false;

    void Start()
    { 
        DontDestroyOnLoad(gameObject);
        currentHealth = maxHealth;
        currentFood = maxFood;
        currentWater = maxWater;

        StartCoroutine(DecreaseFoodOverTime());
        StartCoroutine(DecreaseWaterOverTime());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { EatItemFromInventory(0); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { EatItemFromInventory(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { EatItemFromInventory(2); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { EatItemFromInventory(3); }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) { EatItemFromInventory(4); }

        if (Input.GetKeyDown(KeyCode.C))
        {
            TryPlaceCampfire();
        }
    }

    private void EatItemFromInventory(int slotIndex)
    {
        if (inGameUI.inventorySlots[slotIndex] != null && inGameUI.inventorySlots[slotIndex].sprite == InGameUI.singleton.berrySprite)
        {
            Debug.Log($"Eating berry from slot {slotIndex}.");
            //inGameUI.inventorySlots[slotIndex] = null;
            currentFood = Mathf.Min(currentFood + Mathf.RoundToInt(maxFood * 0.1f), maxFood);
            inGameUI.UpdateInventoryUI(slotIndex, null); 
            inGameUI.UpdateUI();
        }
        else
        {
            Debug.Log("No berry found in selected slot.");
        }
    }

    public void AddWoodToInventory()
    {
        for (int i = 0; i < inGameUI.inventorySlots.Length; i++)
        {
            if (inGameUI.inventorySlots[i] != null)
            {
                if(inGameUI.inventorySlots[i].GetComponent<Image>().sprite == InGameUI.singleton.emptySlotSprite)
                {
                    // inGameUI.inventorySlots[i] = new GameObject { tag = "Wood" };
                    //inGameUI.inventorySlots[i].transform.tag = "Wood";
                    Debug.Log(this.gameObject.name + " Old Wood: " + woodCount);
                    woodCount++;
                    Debug.Log(this.gameObject.name + "New Wood: " + woodCount);
                    inGameUI.UpdateInventoryUI(i, "Wood");
                    break;
                }
            }
        }
    }

    public void AddBerryToInventory()
    {
        for (int i = 0; i < inGameUI.inventorySlots.Length; i++)
        {
            if (inGameUI.inventorySlots[i] != null)
            {
                if(inGameUI.inventorySlots[i].GetComponent<Image>().sprite == InGameUI.singleton.emptySlotSprite){
                    //inGameUI.inventorySlots[i] = Instantiate(berryPrefab); 
                    //inGameUI.inventorySlots[i].transform.tag = "Berry";
                    // Add berry count
                    inGameUI.UpdateInventoryUI(i, "Berry");
                    break;
                }
            }
        }
    }
    private void RemoveWoodFromInventory(int count)
    {
        int i = 0;
        foreach (Image inventorySlot in inGameUI.inventorySlots)
        {
            if (inventorySlot.sprite == InGameUI.singleton.woodSprite)
            {
                inGameUI.UpdateInventoryUI(i, null);
            }
            i++;
        }
    }

    private IEnumerator DecreaseFoodOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentFood -= Mathf.RoundToInt(foodDecayRate);
            currentFood = Mathf.Max(currentFood, 0);
            CheckHealthDecreaseCondition();
            inGameUI.UpdateUI();
        }
    }

    private IEnumerator DecreaseWaterOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentWater -= Mathf.RoundToInt(waterDecayRate);
            currentWater = Mathf.Max(currentWater, 0);
            CheckHealthDecreaseCondition();
            inGameUI.UpdateUI();
        }
    }

    private IEnumerator DecreaseHealthOverTime()
    {
        isHealthDecreasing = true;

        while (currentHealth > 0 && (currentFood == 0 || currentWater == 0))
        {
            yield return new WaitForSeconds(5f);
            currentHealth -= 1;
            currentHealth = Mathf.Max(currentHealth, 0);
            inGameUI.UpdateUI();
        }

        isHealthDecreasing = false;
    }

    private void CheckHealthDecreaseCondition()
    {
        if ((currentFood == 0 || currentWater == 0) && !isHealthDecreasing)
        {
            StartCoroutine(DecreaseHealthOverTime());
        }
        else if ((currentFood > 0 || currentWater > 0) && isHealthDecreasing)
        {
            StopCoroutine(DecreaseHealthOverTime());
            isHealthDecreasing = false;
        }
    }

    public void DrinkWater(int amount)
    {
        currentWater = Mathf.Min(currentWater + amount, maxWater); 
        inGameUI.UpdateUI();
        CheckHealthDecreaseCondition();
    }

    private void TryPlaceCampfire()
    {
        if (woodCount >= 3)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 10, LayerMask.GetMask("Terrain")))
            {
                Instantiate(CampfirePrefab, hit.point, Quaternion.identity);
                RemoveWoodFromInventory(3);

                Debug.Log("Campfire placed successfully!");
            }
            else
            {
                Debug.Log("Not valid spot for campfire!");
            }
        }
        else
        {
            Debug.Log("Not enough wood to build a campfire!");
        }
    }

    public bool ConvertBerryToSoup()
    {
    for (int i = 0; i < inGameUI.inventorySlots.Length; i++)
    {
        Image slot = inGameUI.inventorySlots[i];
        
        // Check if the slot contains a berry
        if (slot.sprite == InGameUI.singleton.berrySprite)
        {
            // Replace berry with berry soup
            slot.sprite = InGameUI.singleton.berrySoupSprite;
            inGameUI.UpdateInventoryUI(i, "BerrySoup");
            return true; // Successfully converted
        }
    }

    return false; // No berry found in inventory
    }
}
