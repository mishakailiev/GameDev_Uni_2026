using UnityEngine;
using UnityEngine.UI;

public class LowHealthUI : MonoBehaviour
{
    public SpriteRenderer redOverlay;

    public void ShowLowHealthEffect()
    {
        redOverlay.color = new Color(1, 0, 0, 0.25f);
    }

    public void HideEffect()
    {
        redOverlay.color = new Color(1, 0, 0, 0f);
    }
}