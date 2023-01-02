using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class Item : MonoBehaviour, IInteractable, ISpawnable
{
    public ItemSO ammoStats;
    public int id;
    public string name;
    public ItemType type;
    public int addToValue;
    AudioSource source;
    public TextMeshPro t;
    public VariablesSO pl_pos;
    public GameEventSO respawn;
    public int amountToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        id = ammoStats.id;
        name = ammoStats.name;
        type = ammoStats.type;
        addToValue = ammoStats.addToValue;
        t.text = name;
    }

    void Update()
    {
        //Used to always face the title towards player 
        t.transform.rotation = Quaternion.LookRotation(pl_pos.pos - transform.position);
    }

    public void Interact(GameObject other)
    {
        if (type == ItemType.ammo)
        {
            var inventory = other.GetComponent<Inventory>();
            var match = inventory.weapons.Find((x) => x.GetComponent<WeaponStats>().id == id);

            if (match)
            {
                match.GetComponent<WeaponStats>().bulletCount += addToValue;
                StartCoroutine(DeactivateObject());
            }
               
        }
        else
        {
            other.GetComponent<PlayerStats>().health += addToValue;
            StartCoroutine(DeactivateObject());

        }
            source.PlayOneShot(ammoStats.pickSound);

    }

    public int DeductFromSpawn(int deductFromAmount)
    {
        return amountToSpawn -= deductFromAmount;
    }

    IEnumerator DeactivateObject()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        if (amountToSpawn > 0)
            respawn.Raise();
    }
}
