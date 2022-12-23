using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PatrolPoints))]
public class PatrolPointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PatrolPoints patrolPoint = target as PatrolPoints;

        base.OnInspectorGUI();

        GUILayoutOption opt = GUILayout.MaxWidth(200);

        if (GUILayout.Button("Snap to Surface", opt))
        {
            var allZones = patrolPoint.allZones;
            foreach (var zone in allZones)
            {
                patrolPoint.GetSurface(zone.zones);
            }
            
        }






    }

}
