using System.Collections;
using UnityEngine;

public class PhoneLogic : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [SerializeField] private Vector2 phoneCooldown = new Vector2 (30, 50);
    private float phoneCurrentCooldown;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);
        justRinged = phoneCurrentCooldown;

        standPhoneSounds = GetComponent<PhoneSounds>();

        firstCall = true;

        standPhoneSounds.PlayRingingSound();
        needsPickingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - justRinged > phoneCurrentCooldown && Vector3.Distance(playerPos.transform.position, transform.position) < minimumDistanceForPhone)
        {
            NewPhoneCooldown();
            needsPickingUp = true;
            standPhoneSounds.PlayRingingSound();
        }

        if(Input.GetKeyDown(interactionKey) && Vector3.Distance(playerPos.transform.position, transform.position) < minimumDistanceForInteraction)
        {
            if(needsPickingUp)
            {

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
            handPhoneSounds.PlayHeedMyCallSound();
        }
        else
        {
            int random = Random.Range(0, 2);

            if (random == 0)
            {
                handPhoneSounds.PlayDontMoveSound();
                StartCoroutine(SpawnNpc(State.DontMove, 2f));
            }
            else if (random == 1)
            {
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