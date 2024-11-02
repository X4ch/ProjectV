using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Transform cameraTransform;      // Reference to the camera's transform.
    public float baseShakeDuration = 0.5f; // Base duration of the shake effect.
    public float baseShakeMagnitude = 0.2f; // Base magnitude of the shake (how much the camera shakes).
    public float speedToShakeMultiplier = 0.1f; // Multiplier to scale the shake with speed.

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = cameraTransform.localPosition;
    }

    public void TriggerShake(float speed)
    {
        // Scale the shake duration and magnitude based on the speed.
        float shakeDuration = baseShakeDuration + (speed * speedToShakeMultiplier);
        float shakeMagnitude = baseShakeMagnitude + (speed * speedToShakeMultiplier);
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Vector3 randomPoint = originalPosition + Random.insideUnitSphere * magnitude;
            cameraTransform.localPosition = new Vector3(randomPoint.x, randomPoint.y, originalPosition.z);

            // Use unscaled time so shake stops when Time.timeScale is 0
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition; // Reset the camera position after shaking.
    }
}
