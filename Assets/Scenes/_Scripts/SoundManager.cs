using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour // used referenced youtube videos and Microsoft Copilot
{
    public static SoundManager Instance; // stores instance of sound manager

    private Slider volumeSlider; // stores the slider

    void Awake()
    {
        if (Instance == null) // if sound manager doesnt exist
        {
            Instance = this; // create it
            DontDestroyOnLoad(gameObject); // sound manager should not reset on scene change
        }
        else // if it already exists
        {
            Destroy(gameObject); // Only one instance of sound manager should exist
            return;
        }

        if (!PlayerPrefs.HasKey("Volume")) // if there is no saved volume
            PlayerPrefs.SetFloat("Volume", 0.75f); // set default volume to 0.75

        AudioListener.volume = PlayerPrefs.GetFloat("Volume"); // set volume to saved volume

        Debug.Log("SoundManager initialized with volume: " + AudioListener.volume); // log initial volume for debugging
        Debug.Log(PlayerPrefs.GetFloat("Volume")); // log saved volume for debugging

        SceneManager.sceneLoaded += OnSceneLoaded; // subscribe to scene loaded event to reassign slider when scene changes

        AssignSliderInScene(); // assign slider in case starting in a scene with a slider
    }

    void Update()
    {
        //if (volumeSlider != null)
        //{
            //Debug.Log("Slider live value: " + volumeSlider.value);
        //}
        //int listenerCount = FindObjectsByType<AudioListener>(FindObjectsSortMode.None).Length;

        //Debug.Log(
        //    "Audio Debug → " +
        //    "ListenerVol=" + AudioListener.volume +
        //    " | Pause=" + AudioListener.pause +
        //    " | TimeScale=" + Time.timeScale +
        //    " | Listeners=" + listenerCount
       // );
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // unsubscribe from scene loaded event when sound manager is destroyed to prevent errors
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignSliderInScene(); // reassign slider when scene changes in case the new scene has a slider (like settings) or the old one is destroyed (like main menu)
    }

    public void AssignSliderInScene()
    {
        volumeSlider = null; // reset slider reference

        // Find ALL sliders, including inactive ones
        Slider[] allSliders = Resources.FindObjectsOfTypeAll<Slider>();

        foreach (Slider s in allSliders) // loop through all sliders
        {
            // Skip prefab assets (they have no valid scene)
            if (!s.gameObject.scene.IsValid())
                continue;

            // Skip objects not in the active scene
            if (s.gameObject.scene != SceneManager.GetActiveScene())
                continue;

            // Must have the correct tag
            if (!s.CompareTag("VolumeSlider"))
                continue;

            Debug.Log("Found slider: " + s.name + 
            " | Scene: " + s.gameObject.scene.name + 
            " | Active: " + s.gameObject.activeInHierarchy);

            // This is the correct slider
            volumeSlider = s;
            break;

        }

        if (volumeSlider == null) // if no slider was found
            return; // do nothing

        float saved = PlayerPrefs.GetFloat("Volume"); // get saved volume
        volumeSlider.value = saved; // set slider to saved volume
        Debug.Log("Setting slider to saved value: " + saved);

        volumeSlider.onValueChanged.RemoveAllListeners(); // remove previous listeners to prevent multiple calls
        volumeSlider.onValueChanged.AddListener(SetVolume); // add listener to update volume when slider is changed
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // set volume to slider value
        PlayerPrefs.SetFloat("Volume", volume); // save volume so it persists between sessions
    }
}
