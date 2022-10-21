using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    Animator animator;
    public bool isAiming;
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
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            CameraControl();
            gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * 220 * Time.deltaTime, 0);
            
            if (Input.GetMouseButton(0) && canShoot)
            {

                Shoot();
                
                
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            CameraControl();
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
    }

    void Shoot()
    {
        var weapon = GetComponent<Inventory>().weaponInstance.GetComponent<WeaponStats>();
       

        if (weapon.bulletCount > 0)
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

            weapon.bulletCount--;
            
        }
        else
            source.PlayOneShot(weapon.noAmmo);

        StartCoroutine(CoolDown(weapon.coolDown));

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





}
