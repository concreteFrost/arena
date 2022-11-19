using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    ammo,
    medicine
}
[CreateAssetMenu(menuName ="Items/new item")]
public class ItemSO : ScriptableObject
{
    public int id;
    public string name;
    public int addToValue;
    public ItemType type;
    public AudioClip pickSound;
}
