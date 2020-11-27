using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireDirection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cube; // the bullet
    public float speed;
    public Transform pivotShootTransform = null;
    public bool reload = false;
    
    [SerializeField] private List<Guns> gunsList = new List<Guns>();
    [SerializeField] private float cooldwonShoot = 0.5f;

    private AnimationStates animationState = null;
    private bool fireRate = true;
    private int ammoCounter= 28;
    private int currentGunIndex = 0;

    private void Awake()
    {
        foreach (Guns gun in gunsList)
        {
            gun.Init();
        }

        ammoCounter = gunsList[0].currentAmmoCounter;
        animationState = GetComponent<AnimationStates>();
        Relowad();
        gunsList[currentGunIndex].gunRef.SetActive(true);
        //gunsList[currentGunIndex].gunImage.gameObject.SetActive(true);
    }


    void Update()
    {
        //RotatePo();
        WhenPlayerShoot();
        WeaponSwitch();
    }

    // shoot to  the right position
    public void CheckClick()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (Input.GetMouseButton(0))
        {
            Ray camerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(camerRay, out hit,1000, LayerMask.GetMask("ShootZone")))
            {

                Vector3 vector3 = new Vector3(hit.point.x, transform.position.y , hit.point.z);

                if (Input.GetMouseButton(3))
                {

                    //transform.LookAt(vector3);
                }
                
                    var bullet =Instantiate(gunsList[currentGunIndex].bulletPrefab, pivotShootTransform.position, Quaternion.identity);
                    var bulletVar = bullet.GetComponent<Bullet>();
                    Vector3 newHitPoint = new Vector3(hit.point.x , hit.point.y , hit.point.z);
                    bulletVar.hitPoint = newHitPoint;
                    //Debug.Log(hit.point);
                    bulletVar.OnStart();
                
                gunsList[currentGunIndex].currentAmmoCounter--;
                GameManager.instance.BulletResiver(gunsList[currentGunIndex].currentAmmoCounter, gunsList[currentGunIndex].maxAmmo);

                Relowad();
                StartCoroutine(CooldownBetweenShoot());
            }
        }
    }

    private void RotatePo()
    {
        Ray camerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camerRay, out hit, 1000, LayerMask.GetMask("ShootZone")))
        {
            Vector3 vector3 = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(vector3);
        }
    }

    private void WhenPlayerShoot()
    {
         if (Input.GetMouseButton(1))
         {
           RotatePo();
         }
        animationState.AimAnimation(Input.GetMouseButton(1));
        if (fireRate && !reload)
        {

            if (Input.GetMouseButton(1) && Input.GetMouseButton(0)) {
                animationState.ShootingAnimation(true);
                CheckClick();
                //ammoCounter=gunsList[weaponSwitchIndexCounter].currentAmmoCounter;

            }
            else
            {
                animationState.ShootingAnimation(false);
            }
        }
    }

    IEnumerator CooldownBetweenShoot()
    {
        if (fireRate)
        {
            fireRate = false;
            yield return new WaitForSeconds(gunsList[currentGunIndex].timeBetweenShoot);
            fireRate = true;
        }
    }


    private void WeaponSwitch()
    {
        var lastIndex = currentGunIndex;

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentGunIndex = Guns.SwitchWeapon(KeyCode.A, currentGunIndex, gunsList.Count - 1);
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            currentGunIndex = Guns.SwitchWeapon(KeyCode.E, currentGunIndex, gunsList.Count - 1);
        }

        GameManager.instance.BulletResiver(gunsList[currentGunIndex].currentAmmoCounter, gunsList[currentGunIndex].maxAmmo);

        gunsList[lastIndex].gunRef.SetActive(false);
        gunsList[currentGunIndex].gunRef.SetActive(true);
        gunsList[lastIndex].gunImage.gameObject.SetActive(false);
        gunsList[currentGunIndex].gunImage.gameObject.SetActive(true);
    }


    private void Relowad()
    {

        //ammoCounter = gunsList[index].currentAmmoCounter;

        if (gunsList[currentGunIndex].currentAmmoCounter <= 0)
        {
            reload = true;
            Debug.Log("Current Ammo counter "+ammoCounter);
            StartCoroutine(ReloadCoolDOwn());
        }
       
        
    }


    IEnumerator ReloadCoolDOwn()
    {
        var reloadTime = gunsList[currentGunIndex].gunRoloadTime;
       
        yield return new WaitForSeconds(reloadTime);
        gunsList[currentGunIndex].currentAmmoCounter = gunsList[currentGunIndex].maxAmmo;
        reload = false;
    }

}
