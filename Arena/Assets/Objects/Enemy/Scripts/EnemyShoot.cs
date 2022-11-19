using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Shoot
{
    
    
    public bool waitingTillNextShoot;
    public int bulletsToShoot;

    WeaponStats weaponStats;
    Enemy enemy;
    FieldOfView fov;

    //Field of view
    public bool canSeePlayer;

    float maxFOVAngle = 100;
    public float lookRadius = 300;


    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();
        weaponStats = GetComponent<Enemy>().e_weapon.GetComponent<WeaponStats>();
        enemy = GetComponent<Enemy>();
       
        bulletsToShoot = Random.Range(1, 7);
        waitingTillNextShoot= false;
    }

    // Update is called once per frame
    void Update()
    {
        canSeePlayer = fov.CanSeePlayer(transform, maxFOVAngle, lookRadius, enemy.pl_pos);

        if (canSeePlayer)
        {
            Debug.Log("SSSSS");
        }
    }


    public void Shoot()
    {
       
            if (canShoot && bulletsToShoot >0 && canSeePlayer)
            {

                bulletsToShoot--;
                var errorMargin = 0.3f;
                var targetDir = (enemy.pl_pos.pos + Random.insideUnitSphere * errorMargin) - transform.position;

            PerformShoot(weaponStats, weaponStats.shootingPoint.transform.position, targetDir);
        }

        if (bulletsToShoot <= 0 && !waitingTillNextShoot)
        {
            StartCoroutine(WaitTillNextShoot(Random.Range(0.5f, 2)));
        }

    }

    IEnumerator WaitTillNextShoot(float s)
    {
        waitingTillNextShoot = true;
        yield return new WaitForSeconds(s);
        bulletsToShoot = Random.Range(1, 4);
        waitingTillNextShoot = false;
        
    }


}
