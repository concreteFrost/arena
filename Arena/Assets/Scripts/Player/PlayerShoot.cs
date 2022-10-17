using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    Animator animator;
    public bool isAiming;
    public GameObject cinemachine;
    public bool canShoot;
    public GameObject[] cams;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cams[1].SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            CameraControl();
            gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * 220 * Time.deltaTime, 0);
            var weaponType = GetComponent<Inventory>().weaponInstance.GetComponent<WeaponStats>();
            if (Input.GetMouseButton(0))
            {
                

                
                
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            CameraControl();
        }

        AnimationControl();
    }

    void CameraControl()
    {
        if (isAiming)
        {
            cams[0].SetActive(false);
            cams[1].SetActive(true);
        }
            
        else
        {
            cams[1].SetActive(false);
            cams[0].SetActive(true);
        }
    }

    void AnimationControl()
    {
        animator.SetBool("isAiming", isAiming);
    }

}
