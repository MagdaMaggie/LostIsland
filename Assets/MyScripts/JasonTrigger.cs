using UnityEngine;
using UnityEngine.UI;

public class JasonTrigger : MonoBehaviour
{
    public GameObject winPopupCanvas; 
    public MyFPScript playerMovementScript; 
    private bool isPlayerInRange = false;

    public InGameUI inGameUI;

    void Start()
    {
        if (winPopupCanvas != null)
        {
            winPopupCanvas.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered Jason's trigger area.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited Jason's trigger area.");
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowWinPopup();
        }
    }

    void ShowWinPopup()
    {

        if (winPopupCanvas != null)
        {
            winPopupCanvas.SetActive(true);
        }

        if (inGameUI != null)
        {
            inGameUI.ToggleUI(false); 
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }
}
