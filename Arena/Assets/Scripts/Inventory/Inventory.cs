using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> hats = new List<GameObject>();
    
    //the current weapon that player is holding
    public GameObject weaponInstance;
    
    public Transform weaponHolder;

    PlayerUI ui;
    PlayerShoot plShoot;
    public PlayerStatsSO pl_statsSO;
    public GameObject hat;

    private int _weaponIndex;
    public int weaponIndex
    {
        get { return _weaponIndex; }
        set
        {
            if (value > weapons.Count - 1)
            {
                _weaponIndex = 0;
            }
            else if (value < 0)
            {
                _weaponIndex = weapons.Count - 1;
            }
            else
                _weaponIndex = value;
        }
    }

    private void Start()
    {

        ui = GetComponent<PlayerUI>();
        GetItemOnStart();
        GetWeaponOnStart();
        GetWeaponInfo();
        //Keep this variable to prevent change weapon when player is aiming
        plShoot = GetComponent<PlayerShoot>();
        plShoot.weapon = weaponInstance.GetComponent<WeaponStats>();

    }

    private void Update()
    {
        ChangeWeapon();

    }

    public void AddItem(GameObject w)
    {
        weapons.Add(w);
    }

    void ChangeWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !plShoot.isAiming && !plShoot.isReloading)  
            weaponIndex++;
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !plShoot.isAiming && !plShoot.isReloading) 
            weaponIndex--;
        
        for(int i = 0; i < weapons.Count; i++)
        {
            if (i == weaponIndex)
            {
                weapons[i].SetActive(true);
                weaponInstance = weapons[i];
                weaponInstance.GetComponent<BoxCollider>().enabled = false;
                plShoot.weapon = weaponInstance.GetComponent<WeaponStats>();
            }
                
            else
                weapons[i].SetActive(false);
        }
      
        GetWeaponInfo();
    }

    void GetWeaponInfo()
    {
     
        var bullets = weaponInstance.GetComponent<WeaponStats>();
       ui.bulletAmountText.text = bullets.bulletsInMagazine.ToString() + '/' + bullets.bulletCount.ToString();
        ui.weaponIcon.sprite = weaponInstance.GetComponent<WeaponStats>().weaponImage;
        ui.weaponText.text = weaponInstance.GetComponent<WeaponStats>().weaponName;
    }

    void GetWeaponOnStart()
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("Weapon Holder"))
                weaponHolder = t;
        }
        GameObject w = Instantiate(pl_statsSO.starterWeapon, weaponHolder.position, weaponHolder.rotation);
        w.transform.parent = weaponHolder;
        w.GetComponent<BoxCollider>().enabled = false;
        weaponInstance = w;
        weapons.Add(w);
        
    }

    void GetItemOnStart()
    {
        if(pl_statsSO.additionalItem != null)
        {
            var hatPlacer = GameObject.FindGameObjectWithTag("hatPlacer");

            GameObject i = Instantiate(pl_statsSO.additionalItem, hatPlacer.transform.position, transform.rotation);

            i.transform.parent = hatPlacer.transform;

            hats.Add(i);
        }
    }
}
