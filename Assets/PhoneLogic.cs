using UnityEngine;

public class PhoneLogic : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [SerializeField] private Vector2 phoneCooldown = new Vector2 (30, 50);
    private float phoneCurrentCooldown;

    [SerializeField] private float minimumDistanceForPhone = 15f;
    [SerializeField] private float minimumDistanceForInteraction = 3f;

    [SerializeField] private Transform playerPos;

    private float justRinged = Mathf.Infinity;

    private PhoneSounds phoneSounds;

    private bool needsPickingUp;
    private bool canHangUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);

        phoneSounds = GetComponent<PhoneSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - justRinged > phoneCurrentCooldown && Vector3.Distance(playerPos.position, transform.position) < minimumDistanceForPhone)
        {
            justRinged = Time.time;
            phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);
            needsPickingUp = true;
            phoneSounds.PlayRingingSound();
        }

        if(Input.GetKeyDown(interactionKey) && Vector3.Distance(playerPos.position, transform.position) < minimumDistanceForInteraction)
        {
            if(needsPickingUp)
            {
                needsPickingUp = false;
                canHangUp = true;
                phoneSounds.StopRingingSound();
                phoneSounds.PlayPickUpSound();
                justRinged = Time.time;
                phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);
            }
            else if(canHangUp)
            {
                canHangUp = false;
                phoneSounds.PlayPutDownSound();
                justRinged = Time.time;
                phoneCurrentCooldown = Random.Range(phoneCooldown.x, phoneCooldown.y);
            }
            
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minimumDistanceForPhone);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minimumDistanceForInteraction);
    }
}
