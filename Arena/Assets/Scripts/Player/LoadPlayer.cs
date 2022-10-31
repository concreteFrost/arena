using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
   
    public GameObject plArmature;
    public PlayerStatsSO playerStatsSO;
    public GameObject geometry;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject pl = Instantiate(playerStatsSO.character, transform.position, Quaternion.identity);
        pl.transform.SetParent(geometry.transform);
        plArmature.SetActive(false);
        plArmature.SetActive(true);
    }

}
