using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : FieldOfView
{
    
    WeaponStats weaponStats;

    public bool canShoot = true;
    public bool waitingTillNextShoot;
    public int bulletsToShoot;
    Enemy enemy;

    //Field of view
    public bool canSeePlayer;

    float maxFOVAngle = 100;
    public float lookRadius = 300;


    // Start is called before the first frame update
    void Start()
    {
        
        weaponStats = GetComponent<Enemy>().e_weapon.GetComponent<WeaponStats>();
        enemy = GetComponent<Enemy>();
       
        bulletsToShoot = Random.Range(1, 7);
        waitingTillNextShoot= false;
    }

    // Update is called once per frame
    void Update()
    {
        canSeePlayer = CanSeePlayer(transform, maxFOVAngle, lookRadius, enemy.pl_pos);
    }

    public override bool CanSeePlayer(Transform myPos, float maxAngle, float lookRaduis, VariablesSO pl_pos)
    {
        return base.CanSeePlayer(myPos, maxAngle, lookRaduis, pl_pos);
    }

    public void Shoot()
    {
       
       
            if (canShoot && bulletsToShoot >0 && canSeePlayer)
            {
                weaponStats.WeaponShoot();
                StartCoroutine(CoolDown(weaponStats.coolDown));
                bulletsToShoot--;

                RaycastHit hit;

                var errorMargin = 0.3f;
                Vector3 targetDir = (enemy.pl_pos.pos + Random.insideUnitSphere * errorMargin) - transform.position;
                Debug.DrawRay(weaponStats.shootingPoint.position, targetDir);
                if (Physics.Raycast(weaponStats.shootingPoint.position, targetDir, out hit, weaponStats.shootingRange))
                {
                hit.transform.GetComponent<IDamagable>()?.TakeDamage(weaponStats.weaponDamage);

                Debug.Log(hit.transform.name);
                GameObject hitEffect = Instantiate(weaponStats.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, 1f);
            }
        }

        if (bulletsToShoot <= 0 && !waitingTillNextShoot)
        {
            StartCoroutine(WaitTillNextShoot(Random.Range(0.5f, 2)));
        }

  //      var errorMargin: float = 1.0;
  //      ...
  //// when calculating the shot direction:
  //var shotDir = player.position + Random.insideUnitsphere * errorMargin - spawnPoint.position;


    }


    IEnumerator CoolDown(float s)
    {
        canShoot = false;
        yield return new WaitForSeconds(s);
        canShoot = true;
    }

    IEnumerator WaitTillNextShoot(float s)
    {
        waitingTillNextShoot = true;
        yield return new WaitForSeconds(s);
        bulletsToShoot = Random.Range(1, 4);
        waitingTillNextShoot = false;
        
    }


}
