using UnityEngine;

public class CheckIfPlayerInside : MonoBehaviour
{

    [SerializeField] private GameObject gameWinScreen;
    public bool PlayerInside {  get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        TakePicture takePictureScript = other.GetComponent<TakePicture>();
        if (takePictureScript != null)
            if(takePictureScript.GotAllProof)
            {
                takePictureScript.GetComponent<PlayerMovement>().ToggleMovement(false);
                gameWinScreen.SetActive(true);
            }
                
    }

}
