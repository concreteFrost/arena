using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void AddToHand(GameObject obj)
    {

        obj.transform.SetParent(inventory.weaponHolder);
        obj.transform.position = inventory.weaponHolder.transform.position;
        obj.transform.rotation = inventory.weaponHolder.transform.rotation;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            other.gameObject.GetComponent<IInteractable>().Interact(gameObject);
        }
    }
}
