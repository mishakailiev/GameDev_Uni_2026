using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPos;
    [SerializeField]
    private DamageFlash damageFlash;
    [SerializeField]
    private float shakeDuration = 0.2f;
    [SerializeField]
    private float shakeMagnitude = 0.3f;
    [SerializeField]
    private float intensity = 0.35f;

    public void Shake()
    {
        originalPos = transform.localPosition;
        StartCoroutine(ShakeRoutine());

        damageFlash.Flash(shakeDuration, intensity);
    }

    IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}