using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class ObjectSpawner : SnapToSurface
{
    public List<ObjectsToSpawnSO> objectsToSpawnSO;

    public List<Transform> weaponSpawnPoints;  
    public List<Transform> itemsSpawnPoints;  
    public List<Transform> enemiesSpawnPoints;

    public List<GameObject> weaponsOnStage;
    public List<GameObject> itemsOnStage;
    public List<GameObject> enemiesOnStage;

    public int maxItemsToRespawn;
    public int maxWeaponsToRespawn;

    public void Respawn()
    {
        List<List<GameObject>> list = new List<List<GameObject>>();
        list.Add(weaponsOnStage);
        list.Add(itemsOnStage);

        foreach(var l in list)
        {
            foreach(var item in l)
            {
                if(item.active == false)
                {
                    Debug.Log("here");
                    item.GetComponent<ISpawnable>().DeductFromSpawn(1);
                    StartCoroutine(SetBackToActive(3f, item));      
                    
                }
                    
            }
        }
    }



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
                tempList.Add(tempList[i]);
            }
        }

        ShuffleList(points);

        for (int i = 0; i < points.Count; i++)
        {
            GameObject go;
            go = Instantiate(tempList[i], points[i].position, Quaternion.identity);
            go.transform.parent = points[i];
            go.name = go.name + i;
            listToAdd.Add(go);
        }
    }

    //Use to Place object on a surface
    public override void GetSurface(List<Transform> spawnPositions)
    {
        base.GetSurface(spawnPositions);
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
    
        //for (int i = 0; i < list.Count; i++)
        //{
        //    T temp = list[i];
        //    int rnd = Random.Range(0, list.Count);
        //    list[i] = list[rnd];
        //    list[rnd] = temp;

        //}

        foreach(T item in list)
        {
            Debug.Log(item);
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
            if (t != null)
            {

                Gizmos.color = col;
                Gizmos.DrawSphere(t.position, 0.2f);
            }
        }
    }

    IEnumerator SetBackToActive(float s, GameObject go)
    {
        yield return new WaitForSeconds(s);
        go.SetActive(true);

    }
}
