using UnityEngine;

public class ToggleArmsMesh : MonoBehaviour
{
    [SerializeField] private GameObject arms;

    public void ActivateArms() => arms.SetActive(true);

    public void DeactivateArms() => arms.SetActive(false);
}
