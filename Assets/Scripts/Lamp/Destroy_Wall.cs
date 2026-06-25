using UnityEngine;

public class Destroy_Wall : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyable_object;
    [SerializeField]
    private Animator animation;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (destroyable_object != null)
            {
                Destroy(destroyable_object);
                animation.SetTrigger("Light");
            }
            else
            {
                Debug.Log("No Object to destroy!");
            }
        }
    }
}
