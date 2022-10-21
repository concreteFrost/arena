using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCamera : MonoBehaviour
{
    [Range(0.1f, 9f)]
    [SerializeField]
    float _sensitivity = 2f;

    [SerializeField]
    float x_sensitivity = 10f;
    [Tooltip("Limit vertical cam position")]

    [Range(0, 90f)]
    [SerializeField]
    float yRotationLimit = 88f;

    public Vector3 rotation = Vector3.zero;
    GameObject spineRotateTarget;

    public float recoilY = 1;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        spineRotateTarget = GameObject.FindGameObjectWithTag("Player Spine Rotate Target");
        spineRotateTarget.transform.parent = gameObject.transform;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
      
        rotation.y += Input.GetAxis("Mouse Y") * _sensitivity + recoilY;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        //var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);

        if(rotation.x != 0)
        {
            if (rotation.x > 0)
                rotation.x -= Time.deltaTime * 2;
            else
                rotation.x += Time.deltaTime * 2;
           
        }

        transform.localRotation = yQuat * xQuat;
    }

 

    private void OnEnable()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnDisable()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
