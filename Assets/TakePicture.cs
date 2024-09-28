using UnityEngine;

public class TakePicture : MonoBehaviour
{

    private Camera cam;

    private RenderTextureCapture rtc;

    private Sprite lastPicture;

    [SerializeField] private GameObject poraloid;

    [SerializeField] private RenderTexture captureTexture;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        rtc = GetComponent<RenderTextureCapture>();

    }

    // Update is called once per frame
    void Update()
    {


        //if (Input.GetMouseButtonDown(0))
        //{

        //        CameraShooting();


        //}

    }




    public void CameraShooting()
    {
        rtc.GetLatestPhoto();
        poraloid.GetComponent<Renderer>().material.SetTexture("_BaseMap", rtc.toTexture2D(captureTexture));
    }



}
