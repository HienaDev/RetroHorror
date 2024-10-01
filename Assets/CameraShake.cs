using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // How long the shake effect lasts
    public float shakeDuration = 0.5f;

    // Strength of the shake movement
    public float shakeMagnitude = 0.2f;

    // Speed at which the shake effect will diminish
    public float dampingSpeed = 1.0f;

    // The original position of the camera
    private Vector3 initialPosition;

    private float shakeTimeRemaining;

    void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.localPosition;

    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            // Shake the camera by adding a random offset to the position
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            // Reduce the shake time remaining
            shakeTimeRemaining -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // Reset the camera position to the initial position after the shake is over
            transform.localPosition = initialPosition;
        }
    }

    // Public method to trigger the shake effect
    public void TriggerShake(float duration)
    {
        shakeTimeRemaining = duration;
    }
}
