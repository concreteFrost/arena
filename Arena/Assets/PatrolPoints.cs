using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedLists
{
    public List<Transform> zones;
    public Color zoneGizmoColor;
    public int zoneIndex;
   
}

[ExecuteInEditMode]
public class PatrolPoints : SnapToSurface
{

    public List<SerializedLists> allZones = new List<SerializedLists>();  
    
    private void Start()
    {
       for(int i = 0; i < allZones.Count; i++)
        {
            allZones[i].zoneIndex = i;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        foreach (var zones in allZones)
        {
            foreach(var zone in zones.zones)
            {
                Gizmos.color = zones.zoneGizmoColor;
               
                Gizmos.DrawWireCube(zone.transform.position, new Vector3(1, 1, 1));
            }
        }

    }

    private void AssignGizmos(List<Transform> zone, Color color)
    {
        foreach(var z in zone)
        {
            if(z != null)
            {
                Gizmos.color = color;
                Gizmos.DrawWireCube(z.transform.position,new Vector3(1,1,1));
            }
        }
    }

    public override void GetSurface(List<Transform> spawnPositions)
    {
        base.GetSurface(spawnPositions);
    }




}
