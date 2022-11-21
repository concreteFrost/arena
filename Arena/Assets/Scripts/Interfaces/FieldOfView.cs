using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public bool CanSeePlayer(Transform myPos, float maxAngle, float lookRaduis, VariablesSO pl_pos, string targetName)
    {
        Vector3 targetDir = pl_pos.pos - myPos.position;
        var dist = Vector3.Distance(pl_pos.pos, myPos.position);
        float angleToPlayer = (Vector3.Angle(targetDir, myPos.forward));

        if (angleToPlayer >= -maxAngle && angleToPlayer <= maxAngle && dist < lookRaduis)
        {
            RaycastHit hit;
            var targetCenter = new Vector3(targetDir.x, 0.5f, targetDir.z);
            if (Physics.Raycast(myPos.position, targetCenter, out hit))
            {
                if (hit.collider.CompareTag(targetName))
                {

                    return true;

                }

            }

        }

        return false;

    }

}
