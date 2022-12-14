 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    Inventory inventory;
    PlayerUI playerUI;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        playerUI = GetComponent<PlayerUI>();
    }

    private void Update()
    {
        RescueInteract();
       
    }

    public void RescueInteract()
    {

        RaycastHit hit;



        if (Physics.SphereCast(transform.position + Vector3.up, 1f, transform.forward, out hit, 3f))
        {
            if (hit.transform.GetComponent<RescueManager>())
            {
                playerUI.rescueCommands.SetActive(true);
                var rescue = hit.transform.GetComponent<RescueManager>();
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    rescue.Wait();
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    rescue.Follow(transform);
                if (Input.GetKeyDown(KeyCode.Alpha3))
                    rescue.TakeCover();

            }

           

        }
        else playerUI.rescueCommands.SetActive(false);




    }

    //This method is called within Interactable object
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
