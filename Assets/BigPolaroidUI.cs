using UnityEngine;

public class BigPolaroidUI : MonoBehaviour
{

    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private FirstPersonCamera firstPersonCamera;
    [SerializeField] private LightningStrike takePicture;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {


            Cursor.lockState = CursorLockMode.None;
            playerMove.enabled = false;
            firstPersonCamera.enabled = false;
            takePicture.canTakePictures = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
