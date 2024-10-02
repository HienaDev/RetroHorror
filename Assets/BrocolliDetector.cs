using UnityEngine;

public class BrocolliDetector : MonoBehaviour
{
    [SerializeField] private AudioSource dance;
    [SerializeField] private AudioSource watchThis;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>() != null)
        {
            watchThis.Play();
            dance.volume = 0.3f;
            dance.time = 0.0f;
        }
            
    }
}
