using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCamera : MonoBehaviour
{
    [Range(0.1f, 9f)]
    [SerializeField]
    float _sensitivity = 2f;
    [Tooltip("Limit vertical cam position")]

    [Range(0, 90f)]
    [SerializeField]
    float yRotationLimit = 88f;

    Vector3 rotation = Vector3.zero;

    GameObject spineRotateTarget;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        spineRotateTarget = GameObject.FindGameObjectWithTag("Player Spine Rotate Target");
        spineRotateTarget.transform.parent = gameObject.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x += Input.GetAxis("Mouse X") * _sensitivity;
        rotation.y += Input.GetAxis("Mouse Y") * _sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        //var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = yQuat;
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
