using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnimationOpacity : MonoBehaviour
{

    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Animation()
    {

        float lerpValue = 0f;

        while(lerpValue <= 1f)
        {
            lerpValue += Time.deltaTime / fadeInDuration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, lerpValue);
            yield return null;
        }



        yield return new WaitForSeconds(duration);

        while (lerpValue >= 0f)
        {
            lerpValue -= Time.deltaTime / fadeOutDuration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, lerpValue);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
