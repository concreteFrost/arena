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

            var  c = inventory.weapons.Any((x)=>x.GetComponent<Weapon>().id == other.GetComponent<Weapon>().id);
            
            if (!c)
            {
                var weaponIndex = other.GetComponent<Weapon>().id;
                var db = GameObject.FindGameObjectWithTag("DataBase").GetComponent<Database>();

                var w = inventory.weapons.Find((x) => x.GetComponent<Weapon>().id == weaponIndex);
                inventory.AddItem(w);
                Destroy(other.gameObject);
            }
        }
    }
}
