using UnityEngine;

public class GroundDisappear : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("GroundDisappear collided with: " + collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
