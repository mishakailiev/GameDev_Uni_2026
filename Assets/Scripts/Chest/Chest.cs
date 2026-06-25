using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField]
    private Animator animation;
    [SerializeField]
    private GameObject item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        item.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            animation.SetTrigger("open");
        }
    }

    public void PopItem()
    {
        item.SetActive(true);
    }

}
