using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    // Start is called before the first frame update

    private void Update()
    {
        foreach (var weapon in weapons)
        {
            if(weapon.GetComponent<BoxCollider>() != null)
            {
                weapon.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void AddItem(GameObject w)
    {
        weapons.Add(w);
    }


}
