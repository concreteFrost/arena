using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPoint :  FieldOfView
{
   
    
    float maxFOVAngle = 100;
    public float lookRadius = 1000;
    public bool canSeePlayer;
    public VariablesSO pl_pos;

    public override bool CanSeePlayer(Transform myPos, float maxAngle, float lookRaduis, VariablesSO pl_pos)
    {
        return base.CanSeePlayer(myPos, maxAngle, lookRaduis, pl_pos);
    }



    // Update is called once per frame
    void Update()
    {
        canSeePlayer = CanSeePlayer(transform, maxFOVAngle, lookRadius, pl_pos);
        
    }
}
