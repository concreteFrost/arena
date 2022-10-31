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
    public Vector3 defSpineTargetPos;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        spineRotateTarget = GameObject.FindGameObjectWithTag("Player Spine Rotate Target");
        defSpineTargetPos = spineRotateTarget.transform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
     
        rotation.y += Input.GetAxis("Mouse Y") * _sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        spineRotateTarget.transform.position = new Vector3(spineRotateTarget.transform.position.x, rotation.y /2, spineRotateTarget.transform.position.z);
   
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

        rotation.y = 0;
    }

    private void OnDisable()
    {
        rotation.y = 0;
        if(spineRotateTarget != null)
        spineRotateTarget.transform.localPosition = defSpineTargetPos;

    }
}
