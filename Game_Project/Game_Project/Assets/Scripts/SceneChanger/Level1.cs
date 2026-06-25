using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Level1 : MonoBehaviour
{
    [SerializeField] private Transform player_position;
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && Vector2.Distance(player_position.position, transform.position) < 2f)
        {
            Debug.Log("Player is near the finish point and pressed vertical input. Loading Level 2...");
            LoadLevel2();
        }
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
}
