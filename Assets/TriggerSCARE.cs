using UnityEngine;

public class TriggerSCARE : MonoBehaviour
{

    [SerializeField] private GameObject handJumpScare;
    [SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>() != null)
        {
            handJumpScare.SetActive(true);
            animator.SetTrigger("Trugger");
        }
    }
}
