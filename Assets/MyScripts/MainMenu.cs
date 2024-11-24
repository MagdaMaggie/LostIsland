using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas; 
    public GameObject inGameUICanvas; 

    public GameObject playerPrefab;
    public GameObject mainMenuCamera;

    public Transform StartPosition;

    public void StartGame()
    {

        // Hide the Main Menu Canvas
        mainMenuCanvas.SetActive(false);

        // Show the In-Game UI Canvas
        inGameUICanvas.SetActive(true);

        Destroy(mainMenuCamera);

        GameObject player = Instantiate(playerPrefab, StartPosition.position, Quaternion.identity);
        player.GetComponent<PlayerStats>().inGameUI=inGameUICanvas.GetComponent<InGameUI>();


        // Add any other actions needed to start the game
        Debug.Log("Game Started!");
    }

    // Function for the options button
    public void OpenOptions()
    {
        // This function will later open the options menu.
        Debug.Log("Options button pressed.");
    }

    // Function to quit the game
    public void QuitGame()
    {
        // Exits the application
        Application.Quit();
        Debug.Log("Game is exiting.");
    }
}
