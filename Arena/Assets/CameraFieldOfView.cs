using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFieldOfView : MonoBehaviour
{
    FieldOfView fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
