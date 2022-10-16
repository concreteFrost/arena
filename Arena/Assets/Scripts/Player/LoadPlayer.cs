using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{

    public static GameObject player;
    public GameObject plArmature;
    GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
      
      pl.transform.SetParent(transform);
        plArmature.SetActive(false);
        plArmature.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
