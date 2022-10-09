using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeView : MonoBehaviour
{

    public List<Transform> cam_points = new List<Transform>();
    public Transform lookAt;
    public int pointIndex;
   
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {

        cam.transform.LookAt(lookAt);
        cam.transform.position = Vector3.Lerp(cam.transform.position, cam_points[pointIndex].position, Time.deltaTime * 2f);
       

    }

    public void IncrementIndex()
    {
        if (pointIndex >= cam_points.Count - 1)
            pointIndex = 0;
        else
            pointIndex += 1;
    }

    public void DecrementIndex()
    {
        if (pointIndex <= 0)
            pointIndex = cam_points.Count - 1;
        else
            pointIndex -= 1;
    }




}
