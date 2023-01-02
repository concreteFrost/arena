using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueManager : MonoBehaviour
{
    Rescue rescue;
 
    // Start is called before the first frame update
    void Start()
    {
        rescue = GetComponentInParent<Rescue>();

    }

    private void Update()
    {
        
    }




    public void Wait()
    {
        rescue.anim.SetBool("isFollowing", false);

    }

    public void Follow(Transform targetToLookAt)
    {
        rescue.anim.SetBool("isFollowing", true);
        rescue.anim.SetBool("isCovering", false);
        rescue.targetToLookAt = targetToLookAt;
       

    }

    public void TakeCover()
    {
        rescue.anim.SetBool("isCovering", true);
        rescue.anim.SetBool("isFollowing", false);
    }

}

