using UnityEngine;

public class VictoryAudio : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        }
        audioSource.Play();
    }

}
