using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float bulletLifeTime = 5f;


    IEnumerator DestroyBullet()
    {
        //destroy bullet after a period of time
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.layer != 8)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }
}
