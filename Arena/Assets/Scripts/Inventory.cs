using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public Image weaponIcon;
    public GameObject weaponInstance;
    public int _weaponIndex;
    Transform weaponHolder;
    Database db;
    PlayerShoot plShoot;
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

        db = GameObject.FindGameObjectWithTag("DataBase").GetComponent<Database>();
        //Find Weapon Holder in Child
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("Weapon Holder"))
                weaponHolder = t;
        }
        weapons.Add(db.weapons[PlayerPrefs.GetInt("Weapon On Start")]);
        weaponInstance = Instantiate(db.weapons[PlayerPrefs.GetInt("Weapon On Start")], weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.parent = weaponHolder;
        weaponIcon.sprite = weaponInstance.GetComponent<WeaponStats>().weaponImage;
        plShoot = GetComponent<PlayerShoot>();

    }

    private void Update()
    {
        ChangeWeapon();

        foreach (var weapon in weapons)
        {
            if (weapon.GetComponent<BoxCollider>() != null)
            {
                weapon.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void AddItem(GameObject w)
    {
        weapons.Add(w);
    }

    void ChangeWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !plShoot.isAiming)  
            weaponIndex++;
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !plShoot.isAiming) 
            weaponIndex--;

        Destroy(weaponInstance);
        weaponInstance = Instantiate(weapons[weaponIndex], weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.parent = weaponHolder;
        weaponIcon.sprite = weaponInstance.GetComponent<WeaponStats>().weaponImage;
    }


}
