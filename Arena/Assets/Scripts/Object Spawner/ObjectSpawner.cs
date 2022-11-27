using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class ObjectSpawner : MonoBehaviour
{
    public List<ObjectsToSpawnSO> objectsToSpawnSO;

    public List<Transform> weaponSpawnPoints;
    public List<Transform> itemsSpawnPoints;
    public List<Transform> enemiesSpawnPoints;

    public List<GameObject> weaponsOnStage;
    public List<GameObject> itemsOnStage;
    public List<GameObject> enemiesOnStage;


    public void Spawn(ObjectsToSpawnSO objectsToSpawn, List<GameObject> listToAdd, List<Transform> points)
    {
        ResetObjects(listToAdd);
        GetSurface(points);

        var tempList = new List<GameObject>(objectsToSpawn.objects);


        //If Object list is smaller than point list 
        //then add Missed Slots
        if(tempList.Count < points.Count)
        {
            var diff = points.Count - tempList.Count;
            
            for(int i = 0; i < diff; i++)
            {
                tempList.Add(tempList[Random.Range(0, tempList.Count-1)]);
            }
        }

        ShuffleList(tempList);

        for (int i = 0; i < points.Count; i++)
        {
            GameObject go;
            go = Instantiate(tempList[i], points[i].position, Quaternion.identity);
            listToAdd.Add(go);
        }
    }

    //Use to Place object on a surface
    void GetSurface(List<Transform> spawnPositions)
    {
        foreach (var spawnPosition in spawnPositions)
        {
            RaycastHit hit;
            if (Physics.Raycast(spawnPosition.position, -spawnPosition.up, out hit, 1f))
            {
                float yOffset = 0.2f;
                spawnPosition.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            }
        }
    }

    //Use to Delete object from stage
    public void ResetObjects(List<GameObject> listToClear)
    {
        listToClear.ForEach(x => DestroyImmediate(x.gameObject));
        listToClear.Clear();
    }

    //Randomise Items in List
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int rnd = Random.Range(0, list.Count);
            list[i] = list[rnd];
            list[rnd] = temp;

        }
    }



    private void OnDrawGizmos()
    {
        DrawSpawnSpheres(Color.red, enemiesSpawnPoints);
        DrawSpawnSpheres(Color.green, itemsSpawnPoints);
        DrawSpawnSpheres(Color.blue, weaponSpawnPoints);

    
    }

    void DrawSpawnSpheres(Color col, List<Transform> list)
    {
        foreach (var t in list)
        {
            Gizmos.color = col;
            Gizmos.DrawSphere(t.position, 0.2f);
        }
    }
}
