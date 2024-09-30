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

    [SerializeField] private float timeBetweenRinging;

    private PlayerMovement playerScript;

    private float justStep;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceRinging = gameObject.AddComponent<AudioSource>();
        audioSourceRinging.playOnAwake = false;
        //audioSourceRinging.outputAudioMixerGroup = ringingMixer;

        audioSourcePutDown = gameObject.AddComponent<AudioSource>();
        audioSourcePutDown.playOnAwake = false;
        //audioSourcePutDown.outputAudioMixerGroup = putDownMixer;

        audioSourceDontMove = gameObject.AddComponent<AudioSource>();
        audioSourceDontMove.playOnAwake = false;
        //audioSourceDontMove.outputAudioMixerGroup = dontMoveMixer;

        audioSourcePickUp = gameObject.AddComponent<AudioSource>();
        audioSourcePickUp.playOnAwake = false;
        //audioSourcePickUp.outputAudioMixerGroup = pickUpMixer;

        audioSourceHide = gameObject.AddComponent<AudioSource>();
        audioSourceHide.playOnAwake = false;
        //audioSourceHide.outputAudioMixerGroup = hideMixer;

        audioSourcePutDown.spatialBlend = 1;
        audioSourceRinging.spatialBlend = 1;
        audioSourceDontMove.spatialBlend = 1;
        audioSourceHide.spatialBlend = 1;

        audioSourceDontMove.maxDistance = 0.5f;
        audioSourceDontMove.minDistance = 0.5f;
        audioSourceHide.maxDistance = 0.5f;
        audioSourceHide.minDistance = 0.5f;

        audioSourcePutDown.volume = 0.2f;
        audioSourceRinging.volume = 1f;
        audioSourceDontMove.volume = 0.4f;
        audioSourcePickUp.volume = 1f;
        audioSourceHide.volume = 0.4f;


        //audioSourcePickUp.clip = pickUpSound;
        audioSourceRinging.loop = true;

        justStep = timeBetweenRinging;

        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayDontMoveSound();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayHideSound();
        }
    }

    public void PlayRingingSound()
    {

        audioSourceRinging.volume = 1f;
    }

    public void StopRingingSound()
    {

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
}
