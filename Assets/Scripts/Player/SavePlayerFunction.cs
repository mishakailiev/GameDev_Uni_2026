using UnityEngine;  
using System.Collections;

public class SavePlayerFunction : MonoBehaviour{

    public void SavePlayer()
    {
        PlayerHealth health = GetComponent<PlayerHealth>();
        if (health !=  null)
            SaveSystem.SavePlayer(health);
    }

    public void LoadPlayer()
    {

        PlayerData data = SaveSystem.LoadPlayer();
        PlayerHealth health = GetComponent<PlayerHealth>();
        if (data != null)
        {
            health.setHealth(data.health);
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
        }
    }
}
