using UnityEngine;
using System.Collections;

public class TriggerJumpScareScript : MonoBehaviour
{

    [SerializeField] private GameObject jumpScareObject;

    [SerializeField] private GameObject gameOverScreen;

    public void JumpScare()
    {
        jumpScareObject.SetActive(true);
        StartCoroutine(TriggerDeathScreen());
    }

    private IEnumerator TriggerDeathScreen()
    {
        yield return new WaitForSeconds(0);
        gameOverScreen.SetActive(true);
    }
}
