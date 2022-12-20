using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Shoot
{
    public bool waitingTillNextShoot;
    public int bulletsToShoot;

    WeaponStats weaponStats;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {

        weaponStats = GetComponent<Enemy>().e_weapon.GetComponent<WeaponStats>();
        enemy = GetComponent<Enemy>();
        bulletsToShoot = Random.Range(1, 7);
        waitingTillNextShoot = false;
    }

    public void Shoot(Vector3 target)
    {

        if (canShoot && bulletsToShoot > 0 && enemy.canSeePlayer)
        {

            bulletsToShoot--;

            var errorMargin = 0.1f;
            var targetDir = (target + Random.insideUnitSphere * errorMargin) - transform.position;

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
