using UnityEngine;

public class TriggerJumpScareScript : MonoBehaviour
{

    [SerializeField] private GameObject jumpScareObject;



    public void JumpScare() => jumpScareObject.SetActive(true);
}
