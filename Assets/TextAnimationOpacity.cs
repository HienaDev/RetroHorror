using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimationOpacity : MonoBehaviour
{
    [SerializeField] private float initialDelay = 0f;
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    [SerializeField] private bool fadeOut = true;

    private TextMeshProUGUI text;

    private IEnumerator coroutine = null;

    private bool coroutineRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        
    }

    private void OnEnable()
    {
        coroutine = Animation();

        Debug.Log("on enable: " + coroutine);
        if (coroutineRunning)
        {
            Debug.Log("stopped coroutine");
            
            StopCoroutine(coroutine);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        }
        StartCoroutine(coroutine);

        Debug.Log("after enable: " + coroutine);
    }


    private IEnumerator Animation()
    {
        coroutineRunning = true;

        yield return new WaitForSeconds(initialDelay);

        float lerpValue = 0f;

        while(lerpValue <= 1f)
        {
            lerpValue += Time.deltaTime / fadeInDuration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, lerpValue);
            yield return null;
        }


        if(fadeOut)
        {
            yield return new WaitForSeconds(duration);

            while (lerpValue >= 0f)
            {
                lerpValue -= Time.deltaTime / fadeOutDuration;
                text.color = new Color(text.color.r, text.color.g, text.color.b, lerpValue);
                yield return null;
            }

            gameObject.SetActive(false);
        }



        coroutineRunning = false;
    }

}
