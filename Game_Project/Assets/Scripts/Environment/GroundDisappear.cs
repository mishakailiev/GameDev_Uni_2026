using UnityEngine;

public class GroundDisappear : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("GroundDisappear collided with: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "WholePlayer")
        {
            Destroy(gameObject);
        }
    }
}
