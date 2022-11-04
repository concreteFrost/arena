using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPoint : MonoBehaviour
{
   
    
    float maxFOVAngle =45;
    public float lookRadius = 1000;
    public bool canSeePlayer;
    public VariablesSO pl_pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = pl_pos.pos  - transform.position;
        var dist = Vector3.Distance(pl_pos.pos, transform.position);
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

        if (angleToPlayer >= -1200 && angleToPlayer <= 120)
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }
        
    }
}
