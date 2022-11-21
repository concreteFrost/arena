using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : Shoot
{
    Animator animator;
  
    public GameObject cinemachine;
    public GameObject[] cams;
    public GameObject combatCamera;
    public WeaponStats weapon;
    bool playerDead = false;
    //Recoil
    [Range(0, 1)]
    public float recoilX;

    [Range(0, 1)]
    public float recoilY;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cams[1].SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!playerDead)
        {
            CameraControl();
            AimingLogic();
            ReloadingLogic();
            AnimationControl();
        }
    }

    void ReloadingLogic()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (weapon.bulletsInMagazine != weapon.bulletCapacity)
            {
                isReloading = true;
                Reload(weapon);
            }

        }
    }

    void AimingLogic()
    {
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * 220 * Time.deltaTime, 0);

            if (Input.GetMouseButton(0) && canShoot && !isReloading)
            {

                Shoot(weapon);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;

        }
    }

    void CameraControl()
    {
        if (isAiming)
        {
            cams[0].SetActive(false);
            cams[1].SetActive(true);
        }
            
        else
        {
            cams[1].SetActive(false);
            cams[0].SetActive(true);
        }
    }

    void AnimationControl()
    {
        animator.SetBool("isAiming", isAiming);
        animator.SetBool("isGunReloading", isReloading);
    }

    void Shoot(WeaponStats weapon)
    {

        PerformShoot(weapon, weapon.shootingPoint.position, combatCamera.transform.forward);

        if (weapon.bulletsInMagazine > 0)
        {
           
            StartCoroutine(Recoil(weapon));
            
            weapon.bulletsInMagazine--;
            
        }


    }

    public void SetDefaultCamera()
    {
       playerDead = true;
        cams[1].SetActive(false);
        cams[0].SetActive(true);
    }


    IEnumerator Recoil(WeaponStats weapon)
    {
        var c = combatCamera.GetComponent<CombatCamera>();
        c.rotation.y += weapon.recoilPower;
        c.rotation.x += Random.Range(-0.2f, 0.2f);
        yield return null;
    }

    //Calls in the Animation
    public void EndReload()
    {
        isReloading = false;
    }

}
