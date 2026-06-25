using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpamArrow : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform playPosition;
    [SerializeField]
    private float shootInterval = 2f;
    [SerializeField]
    private float shootRange = 10f;

    private float range;

    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            range = Vector2.Distance(transform.position, playPosition.position);
            if(range < shootRange)
            {
                ShootArrow();
                yield return new WaitForSeconds(shootInterval);
            }
            else
            {
                yield return null;
            }
            
        }
    }

    public void ShootArrow()
    {
        if (arrowPrefab != null)
        {
	        if (spawnPoint != null)
            {
		    GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, transform.rotation);
            arrow.GetComponent<EnemyBullet>().player = playPosition;
        	arrow.GetComponent<DestroyBullet>().cam_pos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        	arrow.GetComponent<DestroyBullet>().player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
	        }
            else
            {
                Debug.Log("No spawn point set!");
            }
        }
        else
        {
            Debug.Log("No arrow prefab set!");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
