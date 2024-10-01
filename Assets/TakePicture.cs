using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakePicture : MonoBehaviour
{

    private Camera cam;

    private RenderTextureCapture rtc;

    private Sprite lastPicture;

    [SerializeField] private GameObject poraloid;

    [SerializeField] private RenderTexture captureTexture;

    private HashSet<GameObject> proofsOnCamera = new HashSet<GameObject> ();
    public HashSet<GameObject> ProofsOnCamera => proofsOnCamera;    
    private List<GameObject> alreadyFoundProofs = new List<GameObject>();
    [SerializeField] private int proofsNeededToWin = 8;
    private int proofCount = 0;

    [SerializeField] private float timeToLastText = 2f;

    [SerializeField] private TextMeshProUGUI text;

    private Coroutine savedCoroutine = null;

    [SerializeField] private GameObject polaroidFullObject;

    public bool GotAllProof { get; private set; }

    [SerializeField] private NPCSpawner spawner;
    private bool badGuySpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        rtc = GetComponent<RenderTextureCapture>();

        GotAllProof = false;
    }


    public void CameraShooting()
    {
        if (!polaroidFullObject.activeSelf) polaroidFullObject.SetActive(true);

        rtc.GetLatestPhoto();
        poraloid.GetComponent<Renderer>().material.SetTexture("_BaseMap", rtc.toTexture2D(captureTexture));
        CheckIfProofAlreadyFound();
    }


    private void CheckIfProofAlreadyFound()
    {
        foreach (GameObject check in proofsOnCamera)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(check.transform.position, cam.transform.position - check.transform.position, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(check.transform.position, cam.transform.position - check.transform.position, Color.blue);
                Debug.Log("It hit: " + hit.collider.gameObject.name + " Position: " + hit.point);

                if(hit.collider.gameObject == gameObject)
                {
                    if (proofCount >= proofsNeededToWin)
                    {
                        GotAllProof = true;
                        savedCoroutine = StartCoroutine(ChangeText($"All proofs found. Get to the front door!"));
                    }
                    else if (alreadyFoundProofs.Contains(check))
                    {
                        if(savedCoroutine != null)
                            StopCoroutine(savedCoroutine);

                        savedCoroutine = StartCoroutine(ChangeText("Proof already found"));
                    }
                    else
                    {
                        if (savedCoroutine != null)
                            StopCoroutine(savedCoroutine);

                        alreadyFoundProofs.Add(check);

                        proofCount++;

                        if(proofCount >= proofsNeededToWin * 0.625 && !badGuySpawned)
                        {
                            badGuySpawned = true;
                            spawner.SpawnNPC(State.Here);
                            spawner.GetComponent<PhoneLogic>().StopSpawning();
                        }
                        
                        savedCoroutine = StartCoroutine(ChangeText($"Proof found: {proofCount} out of {proofsNeededToWin}"));
                    }
                }
                else
                    Debug.Log("Did not hit");
            }
        }
    }

    private IEnumerator ChangeText(string sentence)
    {
        text.text = sentence;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        yield return new WaitForSeconds(timeToLastText);

        float lerpValue = 1f;

        while(lerpValue > -0.1f)
        {
            lerpValue -= Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, lerpValue);
            yield return null;
        }

        savedCoroutine = null;
    }

    public void AddToProofs(GameObject proof) => proofsOnCamera.Add(proof);

    public void RemoveFromProofs(GameObject proof) => proofsOnCamera.Remove(proof);

}
