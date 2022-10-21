using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInteract : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IInteractable>() != null)
        {
            var inventory = gameObject.GetComponent<Inventory>();
           
            var  c = inventory.weapons.Any((x)=>x.GetComponent<WeaponStats>().id == other.GetComponent<WeaponStats>().id);
            
            if (!c)
            {    
                inventory.AddItem(other.gameObject);
                other.gameObject.SetActive(false);
                foreach (Transform t in transform.GetComponentsInChildren<Transform>())
                {
                    if (t.CompareTag("Weapon Holder")){
                        other.transform.SetParent(t);
                        other.transform.position = t.transform.position;
                        other.transform.rotation = t.transform.rotation;
                    }
                       
                }


            }
        }
    }
}
