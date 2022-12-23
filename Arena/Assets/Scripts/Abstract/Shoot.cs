using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            //define if its goint to be dust or blood
            int effectType;

            //cause damage if the object has the IDamagable interface
            if (hit.transform.GetComponent<IDamagable>() != null)
            {
                effectType = 1;
                hit.transform.GetComponent<IDamagable>()?.TakeDamage(weaponStats.weaponDamage);
                

            }
            else
                effectType = 0;

            //pool the particle from ObjectPooling script
            if (effectType == 0)
                ObjectPooling.instance.Pool(ObjectPooling.instance.dustParticles, hit);
            else
                ObjectPooling.instance.Pool(ObjectPooling.instance.bloodParticles, hit);

        }

        //sets the interval between shoot
        StartCoroutine(CoolDown(weaponStats.coolDown));

        //turns on muzzle flash and plays an audio clip
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
