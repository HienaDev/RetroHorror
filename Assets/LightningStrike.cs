using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightningStrike : MonoBehaviour
{
    [SerializeField] private Vector2 intensityStrike;
    private float intensityNextStrike;
    [SerializeField] private Vector2 timeToLastStrike;
    private float timeToNextStrike;
    [SerializeField] private Vector2 nextStrikeCooldown;
    private float strikeCooldown;
    private float justStruck;

    private Light lightStrike;

    private bool readyForNextStrike = true;

    [SerializeField] private KeyCode takePhoto = KeyCode.Mouse0;

    [SerializeField] private TakePicture takePicture;
    // Start is called before the first frame update
    void Start()
    {
        lightStrike = GetComponent<Light>();

        intensityNextStrike = Random.Range(intensityStrike.x, intensityStrike.y);
        timeToNextStrike = Random.Range(timeToLastStrike.x, timeToLastStrike.y);
        strikeCooldown = Random.Range(nextStrikeCooldown.x, nextStrikeCooldown.y);

    }

    // Update is called once per frame
    void Update()
    {
        //if(readyForNextStrike && Time.time - justStruck > strikeCooldown)
        //{
        //    readyForNextStrike = false;
        //    StartCoroutine(Strike());
        //}

        if(Input.GetKeyDown(takePhoto))
        {
            StartCoroutine(Strike());
        }
    }

    private IEnumerator Strike()
    {
        //float lerpValue = 0f;

        //while(lerpValue < 1f)
        //{
        //    lerpValue += Time.deltaTime / (timeToNextStrike / 2);
        //    lightStrike.intensity = Mathf.Lerp(0, intensityNextStrike, lerpValue);
        //    yield return null;
        //}

        //while (lerpValue > 0f)
        //{
        //    lerpValue -= Time.deltaTime / (timeToNextStrike / 2);
        //    lightStrike.intensity = Mathf.Lerp(0, intensityNextStrike, lerpValue);
        //    yield return null;
        //}

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
        strikeCooldown = Random.Range(nextStrikeCooldown.x, nextStrikeCooldown.y);
        justStruck = Time.time;
        readyForNextStrike = true;
    }
}
