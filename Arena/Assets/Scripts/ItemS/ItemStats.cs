using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoosterType
{
    health,
    speed,
    stamina
}
public class ItemStats : MonoBehaviour
{
    public string name;
    public float price;
    public BoosterType boosterType;
}
