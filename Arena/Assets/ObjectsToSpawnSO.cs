using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfObject
{
    weapon,
    item,
    enemy
}
[CreateAssetMenu(menuName ="Object Spawner/Objects to Spawn")]
public class ObjectsToSpawnSO : ScriptableObject
{
   
    public List<GameObject> objects;
    public TypeOfObject type;
    public float spawnTimer;
}
