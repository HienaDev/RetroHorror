using UnityEngine;

public class TriggerJumpScare : MonoBehaviour
{



    private void Start()
    {
        JumpScare();
    }

    public void JumpScare()
    {
        GetComponent<Animator>().SetTrigger("JumpScare");
    }
}
