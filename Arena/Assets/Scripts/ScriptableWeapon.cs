using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfWeapon
{
    rifle,
    pistol,
    granade
}

[CreateAssetMenu(menuName ="Weapons/Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    public int id;
    public string weaponName;
    public int weaponDamage;
    public float coolDown;
    public float recoilPower;
    public Sprite weaponImage;
    public TypeOfWeapon type;
    public float shootingRange;
    public AudioClip shotSound;
    public AudioClip noAmmo;

}
