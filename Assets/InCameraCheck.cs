using UnityEngine;

public class InCameraCheck : MonoBehaviour
{
    private Camera cam;
    private MeshRenderer meshRenderer;
    private Plane[] cameraFrustum;
    private Collider col;

    [SerializeField] private float timerToCheckIfInCamera = 0.3f;
    private float justChecked;

    [SerializeField] private float distance = 5f;

    private TakePicture takePictureScript;

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;
        meshRenderer = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        takePictureScript = cam.GetComponentInParent<TakePicture>();

        justChecked = 0f;
    }



    private void FixedUpdate()
    {
        if (Time.time - justChecked > timerToCheckIfInCamera)
        {
            CheckIfInbounds();
            justChecked = Time.time;
        }

        Debug.DrawLine(transform.position, cam.transform.position, Color.yellow);

    }

    private void CheckIfInbounds()
    {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);

        

        if (GeometryUtility.TestPlanesAABB(cameraFrustum, col.bounds) && Vector3.Distance(cam.transform.position, gameObject.transform.position) < distance)
        {
            takePictureScript.AddToProofs(gameObject);
        }
        else
        {
            if (takePictureScript.ProofsOnCamera.Contains(gameObject))
                takePictureScript.RemoveFromProofs(gameObject);
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
