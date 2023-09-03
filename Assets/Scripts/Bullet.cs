using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float bulletLifeTime = 5f;
    [SerializeField]
    private Animator animator;

    private bool isMoving = true;


    IEnumerator DestroyBulletLife()
    {
        //destroy bullet after a period of time
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        if(isMoving)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.layer != 8)
        {
            isMoving = false;
            animator.SetTrigger("isDestroyed");
            Destroy(gameObject, 0.4f);
        }
        
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyBulletLife());
    }
}
