using UnityEngine;
using System.Collections;

public class TriggerJumpScareScript : MonoBehaviour
{

    [SerializeField] private GameObject jumpScareObject;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private CameraShake shakeCamera;

    public void JumpScare()
    {
        jumpScareObject.SetActive(true);
        shakeCamera.TriggerShake(3f);
        StartCoroutine(TriggerDeathScreen());
    }

    private IEnumerator TriggerDeathScreen()
    {
        yield return new WaitForSeconds(0);
        gameOverScreen.SetActive(true);
    }
}
