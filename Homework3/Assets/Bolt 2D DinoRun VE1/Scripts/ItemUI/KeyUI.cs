using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    public Image[] keyimages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < keyimages.Length; i++)
        {
            keyimages[i].gameObject.SetActive(false);
        }
    }
    public int keysCollected = 0;
    
    public void ShowKeyCollected()
    {
        Debug.Log("Key collected! Total keys: " + (keysCollected + 1));
        keyimages[keysCollected].gameObject.SetActive(true);
        keysCollected++;
    }
}
