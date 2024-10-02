using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightningStrike : MonoBehaviour
{

    [SerializeField] private float pictureCooldown = 1f;

    [SerializeField] private Vector2 intensityStrike;
    private float intensityNextStrike;
    [SerializeField] private Vector2 timeToLastStrike;
    private float timeToNextStrike;


    private Light lightStrike;

    private bool readyForNextStrike = true;

    [SerializeField] private KeyCode takePhoto = KeyCode.Mouse0;

    [SerializeField] private TakePicture takePicture;

    [SerializeField] private PlayerSounds playerSounds;

    public bool canTakePictures = true;

    // Start is called before the first frame update
    void Start()
    {
        lightStrike = GetComponent<Light>();

        intensityNextStrike = Random.Range(intensityStrike.x, intensityStrike.y);
        timeToNextStrike = Random.Range(timeToLastStrike.x, timeToLastStrike.y);

    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(takePhoto) && readyForNextStrike && canTakePictures)
        {
            readyForNextStrike = false;
            StartCoroutine(Strike());
            playerSounds.PlayTakePictureSound();
        }
    }

    private IEnumerator Strike()
    {

        lightStrike.intensity = intensityNextStrike;

        yield return new WaitForSeconds(timeToNextStrike * 2);

        lightStrike.intensity = 0;

        yield return new WaitForSeconds(0.1f * 2);

        lightStrike.intensity = intensityNextStrike;
        yield return null;

        takePicture.CameraShooting();

        yield return new WaitForSeconds(0.1f);

        lightStrike.intensity = 0;

        intensityNextStrike = Random.Range(intensityStrike.x, intensityStrike.y);
        timeToNextStrike = Random.Range(timeToLastStrike.x, timeToLastStrike.y);

        yield return new WaitForSeconds(pictureCooldown);

        readyForNextStrike = true;
  
    }
}
