using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainSceneFinish : MonoBehaviour
{
    [SerializeField] private Transform player_position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && Vector2.Distance(player_position.position, transform.position) < 2f)
        {
            Debug.Log("Player is near the finish point and pressed vertical input. Loading Level 1...");
            LoadLevel1();
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
