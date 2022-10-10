using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    PlayerStats stats;

    public float moveX;
    public float moveZ;

    public float smoothTurn = 0.1f;
    float turnSmoothVelocity;

    public float speed;
    public float acceliration;
    Vector3 velocity;

    public bool isGrounded;

    public float jumpHeight;
    float gravity = -19.62f;

    public bool isRunning;
    public bool isCrouched;

    Animator anim;

    public Vector3 previousPosition;

    Transform cam;



    // Start is called before the first frame update
    void Start()
    {
        acceliration = 10;
        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
        speed = stats.speed;
        cam = Camera.main.transform;
       

    }


    private void Update()
    {

        Movement();
        CheckGround();
        AnimationControl();
        
    }

    void Movement()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
            transform.rotation = Quaternion.Euler(0,angle,0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }


        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

       
    }



    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y+0.3f, transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = 0.5f;
        Debug.DrawRay(origin, direction * distance, Color.red);
        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void AnimationControl()
    {
        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;
        anim.SetFloat("moveZ", direction.magnitude);
       
    }
}





