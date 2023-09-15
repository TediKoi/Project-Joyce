using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Properties")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float bulletLifeTime = 5f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int dmg;

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
            //destroy bullet
            isMoving = false;
            animator.SetTrigger("isDestroyed");
            Destroy(gameObject, 0.4f);
        }
        if(other.gameObject.layer == 7)
        {
            //destroy bullet
            isMoving = false;
            animator.SetTrigger("isDestroyed");
            Destroy(gameObject, 0.4f);

            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDmg(dmg);
        }
        
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyBulletLife());
    }
}
