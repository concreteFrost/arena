using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shoot : MonoBehaviour
{
    public bool isAiming;
    public bool isReloading;
    public bool canShoot;
    public virtual void PerformShoot(WeaponStats weaponStats, Vector3 shootPoint,Vector3 direction)
    {
   
        RaycastHit hit;
        if (Physics.Raycast(shootPoint, direction, out hit, weaponStats.shootingRange))
        {
            int effectType = 0;
            if (hit.transform.GetComponent<IDamagable>() != null)
            {
                hit.transform.GetComponent<IDamagable>()?.TakeDamage(weaponStats.weaponDamage);
                effectType = 1;

            }

            GameObject hitEffect = Instantiate(weaponStats.hitEffect[effectType], hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitEffect, 1f);
        }

        StartCoroutine(CoolDown(weaponStats.coolDown));

        weaponStats.WeaponShoot();
    }

    IEnumerator CoolDown(float s)
    {
        canShoot = false;
        yield return new WaitForSeconds(s);
        canShoot = true;
    }


    public virtual void Reload(WeaponStats w)
    {
        if (w.bulletCount > 0 && w.bulletsInMagazine != w.bulletCapacity)
        {
            int amount = w.bulletCapacity - w.bulletsInMagazine;
            amount = (w.bulletCount - amount) >= 0 ? amount : w.bulletCount;
            w.bulletsInMagazine += amount;
            w.bulletCount -= amount;

        }
    }


}
