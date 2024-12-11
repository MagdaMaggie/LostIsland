using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing on the trigger area!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Play the sound when the player enters the trigger area
        if (other.CompareTag("Player") && audioSource != null)
        {
            audioSource.Play();
            Debug.Log($"Playing sound for {gameObject.name}.");
        }
    }
}
