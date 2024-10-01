using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using UnityEngine.UI;

public class AnimationImageOpacity : MonoBehaviour
{
    [SerializeField] private float initialDelay = 0f;
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    [SerializeField] private bool fadeOut = false;
    
    private RawImage image;

    private IEnumerator coroutine = null;

    private bool coroutineRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<RawImage>();

        
    }

    private void OnEnable()
    {
        coroutine = Animation();

        Debug.Log("on enable: " + coroutine);
        if(coroutineRunning)
        {
            Debug.Log("stopped coroutine");
            StopCoroutine(coroutine);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
        StartCoroutine(coroutine);

        Debug.Log("after enable: " + coroutine);
    }



    private IEnumerator Animation()
    {

        coroutineRunning = true;

        yield return new WaitForSeconds(initialDelay);

        float lerpValue = 0f;

        while (lerpValue <= 1f)
        {
            lerpValue += Time.deltaTime / fadeInDuration;
            image.color = new Color(image.color.r, image.color.g, image.color.b, lerpValue);
            yield return null;
        }



        if(fadeOut)
        {

            yield return new WaitForSeconds(duration);

            while (lerpValue >= 0f)
            {
                lerpValue -= Time.deltaTime / fadeOutDuration;
                image.color = new Color(image.color.r, image.color.g, image.color.b, lerpValue);
                yield return null;
            }

            gameObject.SetActive(false);
        }

        coroutineRunning = false;
    }
}
