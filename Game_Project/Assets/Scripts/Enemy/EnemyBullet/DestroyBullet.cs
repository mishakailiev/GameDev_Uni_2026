using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField]
    public Collider2D player;
    [SerializeField]
    public Transform cam_pos;
    [SerializeField]
    private float maxDistance = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.OverlapPoint(transform.position)){
            Debug.Log("Destroyed by dasing! ");
            Destroy(gameObject);
        }
        if ((Vector2.Distance(transform.position, cam_pos.position) > maxDistance)) {
            Destroy(gameObject);
            Debug.Log("Destroyed by camera! ");
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Destroyed by player collision! ");
            Destroy(gameObject);
        }
    }

}
