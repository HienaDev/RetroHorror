using System.Collections;
using UnityEngine;

public class ShowAllPhotos : MonoBehaviour
{
    [SerializeField] private float timeBetweenPhotos = 0.1f;
    private YieldInstruction wfs;

    [SerializeField] private TakePicture takePictureScript;
    [SerializeField] private GameObject polaroidPrefab;
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
            GameObject polaroid = Instantiate(polaroidPrefab);
            polaroid.GetComponentInChildren<PhotoPolaroid>().GetComponent<Renderer>().material.SetTexture("_BaseMap", tex);
            polaroid.transform.position = new Vector3(Random.Range(-5f, 6f), Random.Range(-5f, 6f), 0f) ;  
            yield return wfs;
        }
    }
}
