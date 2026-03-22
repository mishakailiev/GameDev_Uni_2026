using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform player;

    public float spawnY = 20f;
    public float rangeSpawn_min = 6f;
    public float rangeSpawn_max = 3f;

    void Update()
    {
        if (player.position.y > spawnY - 3f)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        float x_1 = Random.Range(player.position.x - rangeSpawn_min, player.position.x - rangeSpawn_max);
        float x_2 = Random.Range(player.position.x + rangeSpawn_max, player.position.x + rangeSpawn_min);
        float x = Random.Range(0, 10);
        if (x % 2 == 0)
        {
            x = x_1;
        }
        else
        {
            x = x_2;
        }

        Instantiate(platformPrefab, new Vector3(x, spawnY, 0), Quaternion.identity);

        spawnY += Random.Range(1.5f, 3f);
    }
}