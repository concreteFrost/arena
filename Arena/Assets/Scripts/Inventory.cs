using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public Image weaponIcon;
    public Text weaponText;
    public Text bulletAmountText;
    public GameObject weaponInstance;
    public int _weaponIndex;
    Transform weaponHolder;
    DataBaseManager db;
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
        db = GameObject.FindGameObjectWithTag("DataBase").GetComponent<DataBaseManager>();

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
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !plShoot.isAiming)  
            weaponIndex++;
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !plShoot.isAiming) 
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
        bulletAmountText.text = weaponInstance.GetComponent<WeaponStats>().bulletCount.ToString();
    }

    void GetWeaponOnStart()
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("Weapon Holder"))
                weaponHolder = t;
        }
        GameObject w = Instantiate(db.weapons[PlayerPrefs.GetInt("Weapon On Start")], weaponHolder.position, weaponHolder.rotation);
       w.transform.parent = weaponHolder;
       w.GetComponent<BoxCollider>().enabled = false;
        weaponInstance = w;
        weapons.Add(w);
        
    }


}
