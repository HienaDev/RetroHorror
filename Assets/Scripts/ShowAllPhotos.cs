using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShowAllPhotos : MonoBehaviour
{
    [SerializeField] private float timeBetweenPhotos = 0.1f;
    private YieldInstruction wfs;

    [SerializeField] private TakePicture takePictureScript;
    [SerializeField] private GameObject polaroidPrefab;
    [SerializeField] private GameObject bigFrameUI;

    [SerializeField] private GameObject parentObjectForPolaroids;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wfs = new WaitForSeconds(timeBetweenPhotos);

        StartCoroutine(showPhotoWithInterval());
    }


    private IEnumerator showPhotoWithInterval()
    {
        foreach(Texture tex in takePictureScript.allPhotosTaken)
        {
            GameObject polaroid = Instantiate(polaroidPrefab, parentObjectForPolaroids.transform);
            polaroid.GetComponentInChildren<PhotoPolaroid>().GetComponent<RawImage>().texture = tex;
            polaroid.GetComponent<UIPolaroidFrame>().SetBigFrame(bigFrameUI);
            polaroid.transform.localPosition = new Vector3( Random.Range(-300f, 300f), Random.Range(-150f, 200f), polaroid.transform.position.z);
            yield return null;
        }
        
    }
}
