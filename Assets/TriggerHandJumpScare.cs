using UnityEngine;

public class TriggerHandJumpScare : MonoBehaviour
{

    [SerializeField] private GameObject handJumpScare;

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
        }
    }
}
