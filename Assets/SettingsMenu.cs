using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (!settings.activeSelf) {
                settings.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                GetComponentInChildren<AudioListener>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponentInChildren<FirstPersonCamera>().enabled = false;
                audioManager.DisableAudioSources();
            }
            else
            {
                settings.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                GetComponentInChildren<AudioListener>().enabled = true;
                GetComponent<PlayerMovement>().enabled = true;
                GetComponentInChildren<FirstPersonCamera>().enabled = true;
                audioManager.EnableAudioSources();
            }
        }
    }
}
