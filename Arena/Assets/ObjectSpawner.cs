using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<ObjectsToSpawnSO> objectsToSpawnSO;
    public List<Transform> weaponSpawnPoints;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GetSurface(weaponSpawnPoints);
        Spawn(objectsToSpawnSO[0], weaponSpawnPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(ObjectsToSpawnSO objectsToSpawn, List<Transform> points)
    {
        foreach(var t in points)
        {
            var randomInstance = objectsToSpawn.objects[Random.Range(0, objectsToSpawn.objects.Count)];
            GameObject weapon = Instantiate(randomInstance, t.position, Quaternion.identity);
       
        }
    }

    void GetSurface(List<Transform> spawnPositions)
    {
        foreach(var spawnPosition in spawnPositions)
        {
            RaycastHit hit;
            if(Physics.Raycast(spawnPosition.position,-spawnPosition.up, out hit, 1f))
            {
                float yOffset = 0.2f;
                spawnPosition.position = new Vector3(hit.point.x,hit.point.y +yOffset, hit.point.z);
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach(var t in weaponSpawnPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(t.position, 0.3f);
        }
    }
}
