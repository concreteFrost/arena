using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataBaseManager : MonoBehaviour
{
    public DataBaseSO db;

    public List<GameObject> weapons;

    private void Start()
    {
        weapons = db.weapons;
    }

}
