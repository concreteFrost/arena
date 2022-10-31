using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> hats = new List<GameObject>();
    public Image weaponIcon;
    public Text weaponText;
    public Text bulletAmountText;
    public GameObject weaponInstance;
    public int _weaponIndex;
    Transform weaponHolder;
    DataBaseManager db;
    PlayerShoot plShoot;
    public PlayerStatsSO pl_statsSO;

    public GameObject hat;
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
        db = GameObject.FindGameObjectWithTag("DataBase").GetComponent<DataBaseManager>();
        GetItemOnStart();
        GetWeaponOnStart();
        GetWeaponInfo();
      
        //Keep this variable to prevent change weapon when player is aiming
        plShoot = GetComponent<PlayerShoot>();

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
            }
                
            else
                weapons[i].SetActive(false);
        }
        GetWeaponInfo();
    }

    void GetWeaponInfo()
    {
        weaponIcon.sprite = weaponInstance.GetComponent<WeaponStats>().weaponImage;
        weaponText.text = weaponInstance.GetComponent<WeaponStats>().weaponName;
        var bullets = weaponInstance.GetComponent<WeaponStats>();
        bulletAmountText.text = bullets.bulletsInMagazine.ToString() + '/' + bullets.bulletCount.ToString();
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

            GameObject i = Instantiate(pl_statsSO.additionalItem, hatPlacer.transform.position, hatPlacer.transform.root.localRotation);

            i.transform.parent = hatPlacer.transform;

            hats.Add(i);
        }
    }
}
