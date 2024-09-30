using UnityEngine;

public class ToggleArmsMesh : MonoBehaviour
{
    [SerializeField] private GameObject arms;
    [SerializeField] private GameObject phoneOnHand;
    [SerializeField] private GameObject phoneOnStand;

    public void ActivateArms()
    {
        arms.SetActive(true);
        phoneOnHand.SetActive(true);
        phoneOnStand.SetActive(false);
    }

    public void DeactivateArms()
    {
        phoneOnHand.SetActive(false);
        arms.SetActive(false);
        Debug.Log(phoneOnHand.name);
        
        phoneOnStand.SetActive(true);
    }
}
