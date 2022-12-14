using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float yOffset;

    public bool CanSeePlayer(Transform myPos, float maxAngle, float lookRaduis, VariablesSO pl_pos, string targetName)
    {
        Vector3 targetDir = pl_pos.pos - myPos.position;
        var dist = Vector3.Distance(pl_pos.pos, myPos.position);
        float angleToPlayer = (Vector3.Angle(targetDir, myPos.forward));

        if (angleToPlayer >= -maxAngle && angleToPlayer <= maxAngle && dist < lookRaduis)
        {
            RaycastHit hit;

            var targetCenter = targetDir;
            if (Physics.Raycast(myPos.position + (Vector3.up * yOffset), targetCenter, out hit))
            {
                Debug.DrawRay(myPos.position + (Vector3.up * yOffset), targetCenter);
                if (hit.collider.CompareTag(targetName))
                {
                    
                    return true;

                }

            }

        }

        return false;

    }

}
