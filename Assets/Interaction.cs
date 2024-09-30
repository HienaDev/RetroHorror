using UnityEngine;

public class Interaction : MonoBehaviour
{

    [SerializeField] private Animator phoneAnimation;
    [SerializeField] private KeyCode showPhone = KeyCode.T;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(showPhone))
        {
            phoneAnimation.SetBool("Calling", !phoneAnimation.GetBool("Calling"));
        }
    }
}
