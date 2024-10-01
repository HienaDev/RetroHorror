using System.Collections;
using UnityEngine;

public class PhoneLogic : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [SerializeField] private Vector2 phoneCooldown = new Vector2 (30, 50);
    private float phoneCurrentCooldown;

    [SerializeField] private float phoneRingMaxDuration = 20f;
    private float callStarted = Mathf.Infinity;
    private bool phonePickedUp = false;
    [SerializeField] private float timeToSpawnMoreMonsters = 5f;
    private float justSpawned = Mathf.Infinity;
    

    [SerializeField] private float minimumDistanceForPhone = 15f;
    [SerializeField] private float minimumDistanceForInteraction = 3f;

    [SerializeField] private PlayerMovement playerPos;

    private float justRinged = Mathf.Infinity;

    private PhoneSounds standPhoneSounds;
    [SerializeField] private PhoneSounds handPhoneSounds;

    private bool needsPickingUp;
    private bool canHangUp;

    private bool firstCall;

    [SerializeField] private NPCSpawner npcSpawner;

    [SerializeField] private GameObject heedMyCalls;
    [SerializeField] private GameObject dontMove;
    [SerializeField] private GameObject hide;

    [SerializeField] private GameObject pickUpPhone;
    [SerializeField] private GameObject putDownPhone;

    private bool badMonsterSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);
        justRinged = phoneCurrentCooldown;
        callStarted = Time.time;
        justSpawned = Time.time;

        standPhoneSounds = GetComponent<PhoneSounds>();

        firstCall = true;

        standPhoneSounds.PlayRingingSound();
        needsPickingUp = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time - callStarted > phoneRingMaxDuration && !phonePickedUp)
        {
            if(Time.time - justSpawned > timeToSpawnMoreMonsters)
            {
                justSpawned = Time.time;
                npcSpawner.SpawnNPC(State.Chase);
            }
            
        }

        if (Time.time - justRinged > phoneCurrentCooldown && Vector3.Distance(playerPos.transform.position, transform.position) < minimumDistanceForPhone)
        {
            NewPhoneCooldown();
            callStarted = Time.time;
            phonePickedUp = false;
            needsPickingUp = true;
            standPhoneSounds.PlayRingingSound();
        }



        if (Input.GetKeyDown(interactionKey) && Vector3.Distance(playerPos.transform.position, transform.position) < minimumDistanceForInteraction)
        {
            if(needsPickingUp)
            {
                phonePickedUp = true;
                NewPhoneCooldown();


                needsPickingUp = false;
                canHangUp = true;
                standPhoneSounds.StopRingingSound();
                standPhoneSounds.PlayPickUpSound();
                
                justRinged = Time.time;
                phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);

                playerPos.ToggleMovement(false);
                playerPos.GetComponentInChildren<Animator>().SetBool("Calling", true);

                StartCoroutine(MonsterSpawnLogic());
            }
            else if(canHangUp)
            {
                NewPhoneCooldown();

                canHangUp = false;
                standPhoneSounds.PlayPutDownSound();
                justRinged = Time.time;
                phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);


                playerPos.ToggleMovement(true);
                playerPos.GetComponentInChildren<Animator>().SetBool("Calling", false);
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(playerPos.transform.position, transform.position) < minimumDistanceForInteraction)
        {
            if(needsPickingUp)
                pickUpPhone.SetActive(true);
            else if(canHangUp)
            {
                pickUpPhone.SetActive(false);
                putDownPhone.SetActive(true);
            }
            else
            {
                if(pickUpPhone.activeSelf)
                    pickUpPhone.SetActive(false);
                if (putDownPhone.activeSelf)
                    putDownPhone.SetActive(false);
            }
        }
        else
        {
            if (pickUpPhone.activeSelf)
                pickUpPhone.SetActive(false);
            if (putDownPhone.activeSelf)
                putDownPhone.SetActive(false);
        }
            
    }

    private void NewPhoneCooldown()
    {
        
        justRinged = Time.time;
        phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);
        Debug.Log("new cooldown is: " + phoneCurrentCooldown);
    }

    private IEnumerator MonsterSpawnLogic()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCall)
        {
            firstCall = false;
            heedMyCalls.SetActive(true);
            handPhoneSounds.PlayHeedMyCallSound();
        }
        else
        {
            int random = Random.Range(0, 2);

            if (random == 0)
            {
                dontMove.SetActive(true);
                handPhoneSounds.PlayDontMoveSound();
                StartCoroutine(SpawnNpc(State.DontMove, 2f));
            }
            else if (random == 1)
            {
                hide.SetActive(true);
                handPhoneSounds.PlayHideSound();
                StartCoroutine(SpawnNpc(State.DontMove, 5f));
            }
        }

    }

    private IEnumerator SpawnNpc(State state, float timer)
    {
        yield return new WaitForSeconds(timer);
        npcSpawner.SpawnNPC(state);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minimumDistanceForPhone);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minimumDistanceForInteraction);
    }
}
