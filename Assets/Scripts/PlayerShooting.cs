using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<Guns> guns;
    [SerializeField] private Transform pivot;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AnimationStates animationState;
    [SerializeField] private float cooldwonShoot=0.5f;

    private bool reload;
    private int gunMaxAmmo;
    private int ammoCounter=15;
    private bool isShooting=true;
    private PlayerController controller;
    
    private void Awake()
    {
        animationState = GetComponent<AnimationStates>();
        ammoCounter = guns[0].maxAmmo;
        controller = GetComponent<PlayerController>();
        //GameManager.instance.BulletResiver(ammoCounter);
    }

    private void Update()
    {
        AttackShoot();
        Relowad();
    }


    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, pivot.position, transform.rotation);
        bullet.GetComponent<Bullet>().OnCreate(controller.GetMouPotion());
    }


    private void AttackShoot()
    {
        animationState.AimAnimation(Input.GetMouseButton(1));

        if (isShooting &&!reload)
        {
            if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
            {
                animationState.ShootingAnimation(true);
                Shoot();
                ammoCounter--;
                GameManager.instance.BulletResiver(ammoCounter);
                StartCoroutine(CooldownBetweenShoot());
            }
            else
            {

                animationState.ShootingAnimation(false);
            }
        }

    }

    IEnumerator CooldownBetweenShoot()
    {
        if (isShooting)
        {
            isShooting = false;
            yield return new WaitForSeconds(cooldwonShoot);
            isShooting = true;
        }
    }

    private void Relowad()
    {
        
        gunMaxAmmo = guns[0].maxAmmo;

        if (ammoCounter <= 0)
        {
            reload = true;
        }
       ;
        StartCoroutine(ReloadCoolDOwn());
    }

    IEnumerator ReloadCoolDOwn()
    {
        var reloadTime = guns[0].gunRoloadTime;
        if (reload == true)
        {
            yield return new WaitForSeconds(reloadTime);
            ammoCounter = gunMaxAmmo;
            reload = false;
        }
    }

}
