using System.Collections;
using UnityEngine;

public class PolaroidAnimation : MonoBehaviour
{

    [SerializeField] private float duration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            lerpValue += Time.deltaTime / duration;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Mathf.Lerp(-0.5f, -0.141f, lerpValue), gameObject.transform.localPosition.z);

            yield return null;
        }
    }
}
