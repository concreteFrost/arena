using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Inventory inventory;
    public GameObject weaponInstance;
    public Transform weaponHolder;

    int _weaponIndex;
    public int weaponIndex
    {
        get { return _weaponIndex; }
        set { 
            if(value > inventory.weapons.Count-1)
            {
                _weaponIndex = 0;
            }
            else if(value < 0)
            {
                _weaponIndex = inventory.weapons.Count-1;
            }
            else
            _weaponIndex = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();

        //Find Weapon Holder in Child
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("Weapon Holder")) 
                weaponHolder = t;
        }
   
        weaponInstance = Instantiate(inventory.weapons[0], weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.parent = weaponHolder;


    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Destroy(weaponInstance);
            weaponIndex++;
            weaponInstance = Instantiate(inventory.weapons[weaponIndex], weaponHolder.position, weaponHolder.rotation);
            weaponInstance.transform.parent = weaponHolder;

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Destroy(weaponInstance);
            weaponIndex--;
            weaponInstance = Instantiate(inventory.weapons[weaponIndex], weaponHolder.position, weaponHolder.rotation);
            weaponInstance.transform.parent = weaponHolder;

        }
    }
}
