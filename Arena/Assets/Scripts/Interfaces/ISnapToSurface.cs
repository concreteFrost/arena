using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISnapToSurface
{
    public void GetSurface(List<Transform> spawnPositions)
    {
        foreach (var spawnPosition in spawnPositions)
        {
            RaycastHit hit;
            if (Physics.Raycast(spawnPosition.position, -spawnPosition.up, out hit, Mathf.Infinity))
            { 
                float yOffset = 0.2f;
                spawnPosition.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            }
        }
    }
}
