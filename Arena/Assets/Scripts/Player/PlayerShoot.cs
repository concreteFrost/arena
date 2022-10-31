using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    Animator animator;
    public bool isAiming;
    public bool isReloading;
    public GameObject cinemachine;
    public bool canShoot = true;
    public GameObject[] cams;
    public Camera combatCamera;
    AudioSource source;
    float coolDown;

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
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        var weapon = GetComponent<Inventory>().weaponInstance.GetComponent<WeaponStats>();
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            CameraControl();
            gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * 220 * Time.deltaTime, 0);
            
            if (Input.GetMouseButton(0) && canShoot && !isReloading)
            {

                Shoot(weapon);     
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            CameraControl();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           
            if(weapon.bulletsInMagazine != weapon.bulletCapacity)
            {
                isReloading = true;
                Reload(weapon);
            }
           
        }

        AnimationControl();
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

        if (weapon.bulletsInMagazine > 0)
        {     
            RaycastHit hit;
            if (Physics.Raycast(combatCamera.transform.position, combatCamera.transform.forward, out hit, weapon.shootingRange))
            {
                hit.transform.GetComponent<IDamagable>()?.TakeDamage(weapon.weaponDamage);
                GameObject hitEffect = Instantiate(weapon.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 1f);
            }
            weapon.muzzleFlash.Play();
            source.PlayOneShot(weapon.shotSound);
            StartCoroutine(Recoil(weapon));

            weapon.bulletsInMagazine--;
            
        }
        else
            source.PlayOneShot(weapon.noAmmo);

        StartCoroutine(CoolDown(weapon.coolDown));

    }

    void Reload(WeaponStats w)
    {
        if(w.bulletCount >=0 && w.bulletsInMagazine != w.bulletCapacity)
        {
            int amount = w.bulletCapacity - w.bulletsInMagazine;
            amount = (w.bulletCount - amount) >= 0 ? amount : w.bulletCount;
            w.bulletsInMagazine += amount;
            w.bulletCount -= amount;
            
        }
    }

    IEnumerator CoolDown(float s)
    {
        canShoot = false;
        yield return new WaitForSeconds(s);
        canShoot = true;
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
