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
    public string i_name;
    public float i_price;
    public BoosterType boosterType;
}
