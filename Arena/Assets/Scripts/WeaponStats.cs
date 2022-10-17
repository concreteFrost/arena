using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeOfWeapon
{
    rifle,
    pistol,
    granade
}
public class WeaponStats : MonoBehaviour, IInteractable
{
    public int id;
    public Sprite weaponImage;
    public TypeOfWeapon type;

    public void Interact()
    {

    }

}
