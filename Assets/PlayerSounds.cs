using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] stepsSound;
    private AudioSource audioSourceSteps;
    [SerializeField] private AudioMixerGroup stepsMixer;

    [SerializeField] private AudioClip[] takePictureSound;
    private AudioSource audioSourceTakePicture;
    [SerializeField] private AudioMixerGroup takePictureMixer;

    [SerializeField] private AudioClip[] blockBreakingSound;
    private AudioSource audioSourceBlockBreaking;
    [SerializeField] private AudioMixerGroup blockBreakingMixer;

    [SerializeField] private AudioClip[] pickUpSound;
    private AudioSource audioSourcePickUp;
    [SerializeField] private AudioMixerGroup pickUpMixer;

    [SerializeField] private AudioClip[] menuSound;
    private AudioSource audioSourceMenu;
    [SerializeField] private AudioMixerGroup menuMixer;

    [SerializeField] private float timeBetweenSteps;

    private PlayerMovement playerScript;

    private float justStep;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceSteps = gameObject.AddComponent<AudioSource>();
        //audioSourceSteps.outputAudioMixerGroup = stepsMixer;

        audioSourceTakePicture = gameObject.AddComponent<AudioSource>();
        //audioSourceTakePicture.outputAudioMixerGroup = takePictureMixer;

        audioSourceBlockBreaking = gameObject.AddComponent<AudioSource>();
        //audioSourceBlockBreaking.outputAudioMixerGroup = blockBreakingMixer;

        audioSourcePickUp = gameObject.AddComponent<AudioSource>();
        //audioSourcePickUp.outputAudioMixerGroup = pickUpMixer;

        audioSourceMenu = gameObject.AddComponent<AudioSource>();
        //audioSourceMenu.outputAudioMixerGroup = menuMixer;

        audioSourceTakePicture.spatialBlend = 1;
        audioSourceSteps.spatialBlend = 1;
        audioSourceBlockBreaking.spatialBlend = 1;

        audioSourceTakePicture.volume = 0.2f;
        audioSourceSteps.volume = 0.07f;
        audioSourceBlockBreaking.volume = 0.4f;
        audioSourcePickUp.volume = 1f;
        audioSourceMenu.volume = 0.4f;


        //audioSourcePickUp.clip = pickUpSound;


        playerScript = GetComponent<PlayerMovement>();

        justStep = timeBetweenSteps;



    }


    private void Update()
    {
        if (Time.time - justStep > timeBetweenSteps)
        {
            
            if (playerScript.GetHasSpeed())
            {
                justStep = Time.time;
                PlayStepsSound();
            }
        }
    }



    public void PlayStepsSound()
    {
        audioSourceSteps.clip = stepsSound[Random.Range(0, stepsSound.Length)];
        audioSourceSteps.pitch = Random.Range(0.95f, 1.05f);

        audioSourceSteps.Play();
    }

    public void PlayTakePictureSound()
    {
        audioSourceTakePicture.clip = takePictureSound[Random.Range(0, takePictureSound.Length)];
        audioSourceTakePicture.pitch = Random.Range(0.95f, 1.05f);

        audioSourceTakePicture.Play();
    }

    public void PlayBlockBreakingSound()
    {
        audioSourceBlockBreaking.clip = blockBreakingSound[Random.Range(0, blockBreakingSound.Length)];
        audioSourceBlockBreaking.pitch = Random.Range(0.95f, 1.05f);

        audioSourceBlockBreaking.Play();
    }

    public void PlayPickUpSound()
    {
        audioSourcePickUp.clip = pickUpSound[Random.Range(0, pickUpSound.Length)];
        audioSourcePickUp.pitch = Random.Range(0.95f, 1.05f);

        audioSourcePickUp.Play();
    }

    public void PlayMenuSound()
    {
        audioSourceMenu.clip = menuSound[Random.Range(0, menuSound.Length)];
        audioSourceMenu.pitch = Random.Range(0.95f, 1.05f);

        audioSourceMenu.Play();
    }

}
