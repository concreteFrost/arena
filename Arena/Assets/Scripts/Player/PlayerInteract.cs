using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInteract : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IInteractable>() != null)
        {
            var inventory = gameObject.GetComponent<Inventory>();
            Debug.Log("done");
            var  c = inventory.weapons.Any((x)=>x.GetComponent<WeaponStats>().id == other.GetComponent<WeaponStats>().id);
            
            if (!c)
            {
                var weaponIndex = other.GetComponent<WeaponStats>().id;
                var db = GameObject.FindGameObjectWithTag("DataBase").GetComponent<Database>();

                var w = db.weapons.Find((x) => x.GetComponent<WeaponStats>().id == weaponIndex);
                inventory.AddItem(w);
                Destroy(other.gameObject);
            }
        }
    }
}
