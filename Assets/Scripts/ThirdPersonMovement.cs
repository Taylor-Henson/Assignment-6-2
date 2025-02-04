using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //animations
    public Animator anim;

    //movement/camera
    public CharacterController controller;
    public Transform cam;
    public bool moving;
    public float turnSmoothTime = 0.3f;
    float turnSmoothVelocity;

    //sprinting and speed
    float speed;
    float normalSpeed = 2.4f;
    float sprintSpeed = 4;

    //gravity
    Vector3 velocity;
    public float gravity = -15f;

    //groundcheck
    public Transform groundCheck;
    public LayerMask ground;
    public bool isGrounded;

    //jumping
    float jumpHeight = 1f;
    public bool isJumping;

    // Start is called once before the first frame
    // rst execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprinting();
        Jumping();
        GroundCheck();
        Gravity();
    }

    #region Movement

    void Movement()
    {
        //get input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //create direction based on input
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //if direction is present and not jumping
        if (direction.magnitude >= 0.1f && !isJumping)
        {
            //moving is if the player is moving, used for other methods
            moving = true;

            //finds angle of player direction to rotate player and converts to degrees, adds camera rotation to sync with camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            //smooths turn using target angle to create angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            //rotates player based on target angle
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //adds camera rotation to player movement
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //move player controller in direction
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        else
        {
            moving = false;
        }       
    }

    #endregion

    #region Sprinting

    void Sprinting()
    {
        if (Input.GetKey("left shift") && moving)
        {
            //sets the player running
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);
            speed = sprintSpeed;
        }
        else if (moving)
        {
            //sets the player walking
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
            speed = normalSpeed;
        }
        else
        {
            //stops running and walking
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            speed = normalSpeed;
        }
    }

    #endregion

    #region Jumping

    void Jumping()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger("Jump");
            isJumping = true;
        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void EndJump()
    {
        print("end jump");
        isJumping = false;
    }

    #endregion

    #region GroundCheck

    void GroundCheck()
    {
        float distance = 0.5f;
        Vector3 position = groundCheck.transform.position;
        Vector3 direction = Vector3.down;
       
        //debug
        Debug.DrawRay(position, direction, Color.green);

        //groundcheck raycast
        isGrounded = Physics.Raycast(position, direction, distance, ground);
        
        //print(isGrounded);
    }

    #endregion

    #region Gravity

    void Gravity()
    {
        //creates velocity based on downwards force over time
        velocity.y += gravity * Time.deltaTime;

        //moves controller down on that velocity
        controller.Move(velocity * Time.deltaTime);

        //resets velocity when on ground
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    #endregion
}
