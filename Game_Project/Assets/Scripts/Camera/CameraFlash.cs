using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    public void Flash(float duration, float intensity)
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine(duration, intensity));
    }

    IEnumerator FlashRoutine(float duration, float intensity)
    {
        Color c = img.color;
        c.a = intensity;
        img.color = c;

        yield return new WaitForSeconds(duration);

        while (img.color.a > 0)
        {
            c = img.color;
            c.a -= Time.deltaTime * 2f;
            img.color = c;
            yield return null;
        }
    }
}