using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Patrol Zone", fileName ="New Patrol Zone ")]
public class PatrolZoneSO : ScriptableObject
{
   public List<Transform> zones = new List<Transform>();
}
