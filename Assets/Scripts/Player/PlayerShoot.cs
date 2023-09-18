using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting")]
    private bool canShoot = true;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bulletDir;
    [SerializeField]
    private float fireRate;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        if (!canShoot) return;
        AudioManager.GetInstance().ShootingSFX(3);
        GameObject bulletSpawn = Instantiate(bullet, bulletDir.position, bulletDir.rotation);
        bulletSpawn.SetActive(true);
        StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
