using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    public List<GameObject> dustParticles = new List<GameObject>();
    public List<GameObject> bloodParticles = new List<GameObject> ();

    public GameObject bloodParticle;
    public GameObject dustParticle;
    public int amountToInstantiate;
    // Start is called before the first frame update

    void Start()
    {
       
        if(instance == null)
        {
            instance = this;
        }

        FillUpList(dustParticles, amountToInstantiate, dustParticle);
        FillUpList (bloodParticles, amountToInstantiate, bloodParticle);
       
    }
    
    //Fill`s up the list with the amount provided
    public void FillUpList(List<GameObject> l, int amount, GameObject obj)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject d = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
            d.transform.parent = transform;
            l.Add(d);
            d.SetActive(false);
        }
    }

    public void Pool(List<GameObject> list, RaycastHit hitPoint)
    {
        //Find the first object that is not active and pool
        var obj = list.FirstOrDefault(x => x.active == false);

        //If all objects are active then instantiate new one
        if (obj == false)
        {
            InstantiateIfNotEnough(list[0], list);
        }

        //Assign pooled object`s position to a hitPoint
        if (obj != null)
        {
            obj.transform.position = hitPoint.point;
            obj.transform.rotation = Quaternion.LookRotation(hitPoint.normal);
            obj.SetActive(true);

            //Deactivate object on timer
            StartCoroutine(SetEffectNotActive(obj));
        }
    }
    //Creates new instance of the object if max amount of used objects exceeded
    public void InstantiateIfNotEnough(GameObject toInstantiate, List<GameObject> listToAdd)
    {
        GameObject obj = Instantiate(toInstantiate, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = transform;
        listToAdd.Add(obj);
        toInstantiate.SetActive(false);

    }

    IEnumerator SetEffectNotActive(GameObject effect)
    {
        yield return new WaitForSeconds(1F);
        effect.SetActive(false);
    }

}
