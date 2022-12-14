using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponStats : MonoBehaviour, IInteractable
{
    public int id;
    public string weaponName;
    public int weaponDamage;
    public float shootingRange;
    public float coolDown;
    public int bulletCount;
    public int bulletsInMagazine;
    public int bulletCapacity;
    public int bulletsOnStart;
    public float recoilPower;
    public Sprite weaponImage;
    public TypeOfWeapon type;
  
    public ParticleSystem muzzleFlash;
    public GameObject[] hitEffect;
 
    AudioSource source;
    public AudioClip shotSound;
    public AudioClip noAmmo;
    public ScriptableWeapon s_weapon;
    public Transform shootingPoint;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        id = s_weapon.id;
        weaponName = s_weapon.weaponName;   
        weaponDamage = s_weapon.weaponDamage;
        shootingRange = s_weapon.shootingRange;
        weaponImage = s_weapon.weaponImage;
        type = s_weapon.type;
        shotSound = s_weapon.shotSound;
        noAmmo = s_weapon.noAmmo;
        coolDown = s_weapon.coolDown;
        recoilPower = s_weapon.recoilPower;
        bulletCapacity = s_weapon.capacity;
        bulletsInMagazine = bulletCapacity;
        bulletCount = s_weapon.capacity;
    }

    private void Update()
    {
        if(transform.parent == null)
            transform.Rotate(Vector3.up * Time.deltaTime * 100f);
    }

    public void Interact(GameObject other)
    {
        var inventory = other.GetComponent<Inventory>();

        //Check if the weapon is not presented in player`s inventory
        var match = inventory.weapons.Any((x) => x.GetComponent<WeaponStats>().id == id);

        if (!match)
        {
            inventory.AddItem(gameObject);
            other.GetComponent<PlayerInteract>().AddToHand(gameObject);
        }
        else
        {
            var sameWeapon = inventory.weapons.FirstOrDefault(x => x.GetComponent<WeaponStats>().id == id);
            sameWeapon.GetComponent<WeaponStats>().bulletCount += bulletCapacity;
            gameObject.SetActive(false);
        }
    }


    public void WeaponShoot()
    {
        if (bulletsInMagazine > 0)
        {
            muzzleFlash.Play();
            source.PlayOneShot(shotSound);
        }
        else
        {
            source.PlayOneShot(noAmmo);

        }

    }

}
