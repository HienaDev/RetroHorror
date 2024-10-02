using UnityEngine;
using UnityEngine.Audio;

public class PhoneSounds : MonoBehaviour
{

    [SerializeField] private AudioClip[] ringingSound;
    private AudioSource audioSourceRinging;
    [SerializeField] private AudioMixerGroup ringingMixer;


    [SerializeField] private AudioClip[] pickUpSound;
    private AudioSource audioSourcePickUp;
    [SerializeField] private AudioMixerGroup pickUpMixer;


    [SerializeField] private AudioClip[] putDownSound;
    private AudioSource audioSourcePutDown;
    [SerializeField] private AudioMixerGroup putDownMixer;

    [SerializeField] private AudioClip[] dontMoveSound;
    private AudioSource audioSourceDontMove;
    [SerializeField] private AudioMixerGroup dontMoveMixer;

    [SerializeField] private AudioClip[] hideSound;
    private AudioSource audioSourceHide;
    [SerializeField] private AudioMixerGroup hideMixer;

    [SerializeField] private AudioClip[] heedMyCallSound;
    private AudioSource audioSourceHeedMyCall;
    [SerializeField] private AudioMixerGroup heedMyCallMixer;

    [SerializeField] private AudioClip[] hereMyCallSound;
    private AudioSource audioSourceHereMyCall;
    [SerializeField] private AudioMixerGroup hereMyCallMixer;

    [SerializeField] private float timeBetweenRinging;

    private PlayerMovement playerScript;

    private float justStep;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceRinging = gameObject.AddComponent<AudioSource>();
        audioSourceRinging.playOnAwake = false;
        //audioSourceRinging.outputAudioMixerGroup = ringingMixer;
        AudioManager.instance.audioSources.Add(audioSourceRinging);

        audioSourcePutDown = gameObject.AddComponent<AudioSource>();
        audioSourcePutDown.playOnAwake = false;
        //audioSourcePutDown.outputAudioMixerGroup = putDownMixer;
        AudioManager.instance.audioSources.Add(audioSourcePutDown);

        audioSourceDontMove = gameObject.AddComponent<AudioSource>();
        audioSourceDontMove.playOnAwake = false;
        //audioSourceDontMove.outputAudioMixerGroup = dontMoveMixer;
        AudioManager.instance.audioSources.Add(audioSourceDontMove);

        audioSourcePickUp = gameObject.AddComponent<AudioSource>();
        audioSourcePickUp.playOnAwake = false;
        //audioSourcePickUp.outputAudioMixerGroup = pickUpMixer;
        AudioManager.instance.audioSources.Add(audioSourcePickUp);

        audioSourceHide = gameObject.AddComponent<AudioSource>();
        audioSourceHide.playOnAwake = false;
        //audioSourceHide.outputAudioMixerGroup = hideMixer;
        AudioManager.instance.audioSources.Add(audioSourceHide);

        audioSourceHeedMyCall = gameObject.AddComponent<AudioSource>();
        audioSourceHeedMyCall.playOnAwake = false;
        //audioSourceHide.outputAudioMixerGroup = hideMixer;
        AudioManager.instance.audioSources.Add(audioSourceHeedMyCall);

        audioSourceHereMyCall = gameObject.AddComponent<AudioSource>();
        audioSourceHereMyCall.playOnAwake = false;
        //audioSourceHide.outputAudioMixerGroup = hideMixer;
        AudioManager.instance.audioSources.Add(audioSourceHereMyCall);

        audioSourcePutDown.spatialBlend = 1;
        audioSourceRinging.spatialBlend = 1;
        audioSourceDontMove.spatialBlend = 1;
        audioSourceHide.spatialBlend = 1;
        audioSourceHeedMyCall.spatialBlend = 1;
        audioSourceHereMyCall.spatialBlend = 1;
        audioSourcePickUp.spatialBlend = 1;

        audioSourceDontMove.maxDistance = 0.5f;
        audioSourceDontMove.minDistance = 0.5f;
        audioSourceHide.maxDistance = 0.5f;
        audioSourceHide.minDistance = 0.5f;
        audioSourceHeedMyCall.maxDistance = 0.5f;
        audioSourceHeedMyCall.minDistance = 0.5f;
        audioSourceHereMyCall.maxDistance = 0.5f;
        audioSourceHereMyCall.minDistance = 0.5f;
        audioSourcePickUp.maxDistance = 0.5f;
        audioSourcePickUp.minDistance = 0.5f;

        audioSourcePutDown.volume = 0.2f;
        audioSourceRinging.volume = 1f;
        audioSourceDontMove.volume = 0.4f;
        audioSourcePickUp.volume = 1f;
        audioSourceHide.volume = 0.4f;
        audioSourceHeedMyCall.volume = 0.4f;
        audioSourceHereMyCall.volume = 0.4f;

        //audioSourcePickUp.clip = pickUpSound;
        audioSourceRinging.loop = true;
        if (ringingSound.Length > 0)
        {
            audioSourceRinging.clip = ringingSound[Random.Range(0, ringingSound.Length)];
            audioSourceRinging.pitch = Random.Range(0.95f, 1.05f);

            audioSourceRinging?.Play();
        }

        justStep = timeBetweenRinging;

        

    }

    private void Update()
    {

    }

    public void PlayRingingSound()
    {
        if(audioSourceRinging != null)
            audioSourceRinging.volume = 1f;
    }

    public void StopRingingSound()
    {
        if (audioSourceRinging != null)
            audioSourceRinging.volume = 0f;
    }

    public void PlayPutDownSound()
    {
        if (putDownSound.Length > 0)
        {
            audioSourcePutDown.clip = putDownSound[Random.Range(0, putDownSound.Length)];
            audioSourcePutDown.pitch = Random.Range(0.95f, 1.05f);

            audioSourcePutDown?.Play();
        }
        
    }


    public void PlayDontMoveSound()
    {
        if(dontMoveSound.Length > 0)
        {
            audioSourceDontMove.clip = dontMoveSound[Random.Range(0, dontMoveSound.Length)];
            audioSourceDontMove.pitch = Random.Range(0.95f, 1.05f);

            audioSourceDontMove?.Play();
        }
        
    }

    public void PlayPickUpSound()
    {
        if (pickUpSound.Length > 0)
        {
            audioSourcePickUp.clip = pickUpSound[Random.Range(0, pickUpSound.Length)];
            audioSourcePickUp.pitch = Random.Range(0.95f, 1.05f);

            audioSourcePickUp?.Play();
        }
            
    }

    public void PlayHideSound()
    {
        if (hideSound.Length > 0)
        {
            audioSourceHide.clip = hideSound[Random.Range(0, hideSound.Length)];
            audioSourceHide.pitch = Random.Range(0.95f, 1.05f);

            audioSourceHide?.Play();
        }
    }

    public void PlayHeedMyCallSound()
    {

        if (heedMyCallSound.Length > 0)
        {

            audioSourceHeedMyCall.clip = heedMyCallSound[Random.Range(0, heedMyCallSound.Length)];
            audioSourceHeedMyCall.pitch = Random.Range(0.95f, 1.05f);

            audioSourceHeedMyCall?.Play();
        }

    }

    public void PlayHereMyCallSound()
    {

        if (hereMyCallSound.Length > 0)
        {

            audioSourceHereMyCall.clip = hereMyCallSound[Random.Range(0, hereMyCallSound.Length)];
            audioSourceHereMyCall.pitch = Random.Range(0.95f, 1.05f);

            audioSourceHereMyCall?.Play();
        }

    }
}
