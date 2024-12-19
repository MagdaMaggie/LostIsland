using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPopup;
    public GameObject mainMenuCanvas; 
    public GameObject inGameUICanvas;
    public GameObject gameOverCanvas;

    public GameObject playerPrefab;
    public GameObject mainMenuCamera;

    public Transform StartPosition;

    private void Start()
    {
        mainMenuCanvas.SetActive(true);
        inGameUICanvas.SetActive(false);
    }

    public void StartGame()
    {


        mainMenuCanvas.SetActive(false);


        inGameUICanvas.SetActive(true);

        Destroy(mainMenuCamera);

        GameObject player = Instantiate(playerPrefab, StartPosition.position, Quaternion.identity);
        InGameUI.singleton.playerStats = player.GetComponent<PlayerStats>();
        InGameUI.singleton.InitializeInventorySlots();
        InGameUI.singleton.UpdateUI();
        player.GetComponent<PlayerStats>().inGameUI = InGameUI.singleton;
        player.GetComponent<PlayerStats>().GameOverCanvas = gameOverCanvas;


        Debug.Log("Game Started!");
    }


    public void OpenOptions()
    {
        mainMenuCanvas.SetActive(false);
        if (optionsPopup != null)
        {
            optionsPopup.SetActive(true); 
            Debug.Log("Options button pressed. Popup opened.");
        }
        else
        {
            Debug.LogError("Options Popup is not assigned in the Inspector.");
        }
    }

    public void CloseOptions()
    {
        if (optionsPopup != null)
        {
            optionsPopup.SetActive(false); 
            Debug.Log("Options popup closed.");
        }
        mainMenuCanvas.SetActive(true);
    }


    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("Game is exiting.");
    }
}
