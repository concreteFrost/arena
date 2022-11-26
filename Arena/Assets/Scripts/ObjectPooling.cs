using System.Collections;
using System.Collections.Generic;
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
    
    public void FillUpList(List<GameObject> l, int amount, GameObject obj)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject d = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
            l.Add(d);
            d.SetActive(false);
        }
    }

    public void InstantiateIfNotEnough(GameObject toInstantiate, List<GameObject> listToAdd)
    {
        GameObject obj = Instantiate(toInstantiate, new Vector3(0, 0, 0), Quaternion.identity);
        listToAdd.Add(obj);
        toInstantiate.SetActive(false);

    }
}
