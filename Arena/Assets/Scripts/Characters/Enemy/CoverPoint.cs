using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPoint : MonoBehaviour
{
   
    float maxFOVAngle = 360;
    public float lookRadius = 1000;
    public bool canSeePlayer;
    public VariablesSO pl_pos;
    FieldOfView fov;

    private void Start()
    {
        fov = GetComponent<FieldOfView>();
    }


    public void Update()
    {
        canSeePlayer = fov.CanSeePlayer(transform, maxFOVAngle, lookRadius, pl_pos, "Player");

        if (canSeePlayer)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    // Update is called once per frame

}
