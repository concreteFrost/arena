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
            int effectType = 0;
            if (hit.transform.GetComponent<IDamagable>() != null)
            {
                hit.transform.GetComponent<IDamagable>()?.TakeDamage(weaponStats.weaponDamage);
                effectType = 1;

            }
            
            if(effectType==0)
                PerformPooling(ObjectPooling.instance.dustParticles, hit);
            else
                PerformPooling(ObjectPooling.instance.bloodParticles, hit);

        }

        StartCoroutine(CoolDown(weaponStats.coolDown));

        weaponStats.WeaponShoot();
    }

    void PerformPooling( List<GameObject> listToUse, RaycastHit hitPoint)
    {
        var obj = listToUse.FirstOrDefault(x => x.active == false);

        if (obj == false)
        {

            ObjectPooling.instance.InstantiateIfNotEnough(listToUse[0], listToUse);
        }

        if (obj != null)
        {
           obj.transform.position = hitPoint.point;
           obj.transform.rotation = Quaternion.LookRotation(hitPoint.normal);
           obj.SetActive(true);
            StartCoroutine(SetEffectNotActive(obj));
        }
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
    IEnumerator SetEffectNotActive(GameObject effect)
    {
        yield return new WaitForSeconds(1F);
        effect.SetActive(false);
    }


}
