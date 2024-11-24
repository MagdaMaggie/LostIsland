using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats singleton;

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

    private bool isHealthDecreasing = false;

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
        DontDestroyOnLoad(gameObject);
        currentHealth = maxHealth;
        currentFood = maxFood;
        currentWater = maxWater;

        inGameUI = InGameUI.singleton;
        inGameUI.InitializeInventorySlots();

        StartCoroutine(DecreaseFoodOverTime());
        StartCoroutine(DecreaseWaterOverTime());
        inGameUI.UpdateUI();
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
        if (inGameUI.inventorySlots[slotIndex] != null && inGameUI.inventorySlots[slotIndex].CompareTag("Berry"))
        {
            Debug.Log($"Eating berry from slot {slotIndex}.");
            inGameUI.inventorySlots[slotIndex] = null;
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
            if (inGameUI.inventorySlots[i] == null)
            {
                if(inGameUI.inventorySlots[i].GetComponent<Image>().sprite != null){                
               // inGameUI.inventorySlots[i] = new GameObject { tag = "Wood" };
                //inGameUI.inventorySlots[i].transform.tag = "Wood";
                
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
            if (inGameUI.inventorySlots[i] == null)
            {
                if(inGameUI.inventorySlots[i].GetComponent<Image>().sprite != null){                  
                //inGameUI.inventorySlots[i] = Instantiate(berryPrefab); 
                //inGameUI.inventorySlots[i].transform.tag = "Berry";
                inGameUI.UpdateInventoryUI(i, "Berry");
                break;
                }
            }
        }
    }
    private void RemoveWoodFromInventory(int count)
    {
        int woodRemoved = 0;

        for (int i = 0; i < inGameUI.inventorySlots.Length; i++)
        {
            if (inGameUI.inventorySlots[i] != null && inGameUI.inventorySlots[i].CompareTag("Wood"))
            {
                inGameUI.inventorySlots[i] = null;
                woodRemoved++;

                if (woodRemoved >= count)
                    break;
            }
        }

        for (int i = 0; i < inGameUI.inventorySlots.Length; i++)
        {
            inGameUI.UpdateInventoryUI(i, inGameUI.inventorySlots[i].gameObject);
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
        int woodCount = 0;

        foreach (var item in inGameUI.inventorySlots)
        {
            if (item != null && item.CompareTag("Wood"))
            {
                woodCount++;
            }
        }

        if (woodCount >= 3)
        {
            Vector3 campfirePosition = transform.forward * 2f;
            campfirePosition.y = 0;
            Quaternion rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            Instantiate(CampfirePrefab, campfirePosition, rotation);
            RemoveWoodFromInventory(3);

            Debug.Log("Campfire placed successfully!");
        }
        else
        {
            Debug.Log("Not enough wood to build a campfire!");
        }
    }
}
