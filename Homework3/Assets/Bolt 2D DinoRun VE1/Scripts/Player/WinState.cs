using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class WinState : MonoBehaviour
{

    public GameObject KeyUI;
    private Collider myCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish" && KeyUI.GetComponent<KeyUI>().keysCollected == 3 && (Input.GetKeyDown("up") || Input.GetButtonDown("Jump"))) {
            Debug.Log("You win!");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
