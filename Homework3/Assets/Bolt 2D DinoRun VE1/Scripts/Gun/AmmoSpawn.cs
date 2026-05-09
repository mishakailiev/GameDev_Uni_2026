using UnityEngine;
using static UnityEngine.Mathf;


public class AmmoSpawn : MonoBehaviour
{
    public GameObject ammoPrefab; // Prefab of the ammo to spawn
    public Transform player;
    public Rigidbody2D playerBody;
    [SerializeField] private float spawnrate = 1f;
    private float nextSpawnTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down") && nextSpawnTime < Time.time)
        {
            SpawnAmmo();
            nextSpawnTime += spawnrate;
        }
    }

    public void SpawnAmmo()
    {
        float dir = Sign(playerBody.linearVelocity.x);
        dir = Mathf.Sign(playerBody.transform.localScale.x);

        Vector3 spawnPosition = player.position + new Vector3(dir * 1f, 0.25f, 0);
        GameObject ammo = Instantiate(ammoPrefab, spawnPosition, Quaternion.identity);
        ammo.transform.localScale = new Vector3(dir, 1, 1);
        ammo.SetActive(true);

        Rigidbody2D ammoRb = ammo.GetComponent<Rigidbody2D>();
        if (ammoRb != null)
        {
            ammoRb.linearVelocity = new Vector2(dir * 2f, 0);
        }
        Debug.Log("object Spawned!");

    }
}
