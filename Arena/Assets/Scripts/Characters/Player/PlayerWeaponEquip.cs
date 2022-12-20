using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponEquip : MonoBehaviour
{
    Inventory inventory;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var weaponType = inventory.weaponInstance.GetComponent<WeaponStats>();

        switch (weaponType.type)
        {
            case TypeOfWeapon.rifle:
                {
                    anim.SetBool("isRifleOn", true);
                    anim.SetBool("isPistolOn", false);
                    anim.SetBool("isGranadeOn", false);
                }
                break;
            case TypeOfWeapon.pistol:
                {
                    anim.SetBool("isRifleOn", false);
                    anim.SetBool("isPistolOn", true);
                    anim.SetBool("isGranadeOn", false);
                }
                break;
            case TypeOfWeapon.granade:
                {
                    anim.SetBool("isRifleOn", false);
                    anim.SetBool("isPistolOn", false);
                    anim.SetBool("isGranadeOn", true);
                }
                break;

        }
    }
}
