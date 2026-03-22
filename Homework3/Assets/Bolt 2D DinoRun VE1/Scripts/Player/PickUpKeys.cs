using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKeys : MonoBehaviour
{
    public Action PickUp;
    public KeyUI keyUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            Debug.Log("Picked up" + other.name);
            Destroy(other.gameObject);
            keyUI.ShowKeyCollected();
        }
    }
}
