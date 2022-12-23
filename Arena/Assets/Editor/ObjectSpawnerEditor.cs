using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ObjectSpawner))]
public class ObjectSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObjectSpawner obj = target as ObjectSpawner;


        base.OnInspectorGUI();
        GUILayoutOption opt = GUILayout.MaxWidth(200);

        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        EditorGUILayout.LabelField("Instantiate Weapons",centeredStyle);


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Weapons", opt))
        {
            obj.Spawn(obj.objectsToSpawnSO[0],obj.weaponsOnStage ,obj.weaponSpawnPoints);
        }
        else if (GUILayout.Button("Items", opt))
        {
            obj.Spawn(obj.objectsToSpawnSO[1], obj.itemsOnStage, obj.itemsSpawnPoints);
        }
        else if (GUILayout.Button("Enemies", opt))
        {
            obj.Spawn(obj.objectsToSpawnSO[2], obj.enemiesOnStage, obj.enemiesSpawnPoints);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Destroy Weapons", opt))
        {
            obj.ResetObjects(obj.weaponsOnStage);
        }

        if (GUILayout.Button("Destroy Items", opt))
        {
            obj.ResetObjects(obj.itemsOnStage);
        }

        if (GUILayout.Button("Destroy Enemies", opt))
        {
            obj.ResetObjects(obj.enemiesOnStage);
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Destroy All", opt))
        {
            DestroyAllItems(obj);
        }

    }



    void DestroyAllItems(ObjectSpawner obj)
    {
        List<List<GameObject>> allItems = new List<List<GameObject>>();
        allItems.Add(obj.weaponsOnStage);
        allItems.Add(obj.itemsOnStage);
        allItems.Add(obj.enemiesOnStage);

        foreach (var list in allItems)
        {
            obj.ResetObjects(list);
        }
    }
}
