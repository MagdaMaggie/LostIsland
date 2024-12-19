using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing on the trigger area!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && audioSource != null)
        {
            audioSource.Play();
           
            Debug.Log("Playing sound for {gameObject.name}.");
        }
    }
}
