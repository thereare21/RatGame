using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float jumpPower = 100f;
    

    [Header("Inputs for movement")]
    [SerializeField]
    private float acceleration = 5f;
    [SerializeField]
    private float deceleration = 0.25f;

    [SerializeField]
    private float maxSpeedGrounded = 5f;
    [SerializeField]
    private float maxSpeedAir = 10f;
    [SerializeField]
    private float airControl = 10f;


    [Header("Gravity")]
    [SerializeField]
    private float normalGravity = 8f;
    [SerializeField]
    private float fallGravity = 10f;


    [Header("Coyote time and jump buffer")]
    [SerializeField]
    private float coyoteTimeBuffer = 0.1f;
    [SerializeField]
    private float jumpTimeBuffer = 0.1f;


    [Header("Ground detection hitbox")]
    [SerializeField]
    private float boxWidth = 0.8f;
    [SerializeField]
    private float boxHeight = 0.05f;
    [SerializeField]
    private float boxDepth = 0.8f;
    [SerializeField]
    private float centerYOffset = -0.5f;


    [SerializeField]
    private float timeSinceJump = 0f;
    [SerializeField]
    private float timeSinceGrounded = 0f;

    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isJumping;

    private Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        isGrounded = false;
        isJumping = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //JUMP INPUT
        if (Input.GetKeyDown(KeyCode.Space)) //set the timer of jumping
        {
            //Debug.Log("Space recorded");
            timeSinceJump = jumpTimeBuffer;
        }
    }

    private void FixedUpdate()
    {

        //LATERAL MOVEMENT
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //input vel is Vector3 in which to move based on the inputs given
        Vector3 inputVel = new Vector3(horizontalInput, 0f, verticalInput).normalized * (isGrounded ? maxSpeedGrounded : maxSpeedAir);

        //Debug.Log("Input: " + inputVel);

        //target vel transforms the input vel vector so that it is relative to the transform.forward
        Vector3 targetVel = transform.TransformDirection(inputVel);

        //Debug.Log("Target velocity magnitude: " + targetVel.magnitude);

        //Debug.Log("Target velocity: " + targetVel);

        //magnitude of difference of target speed - current speed
        //float speedDiff = targetVel.magnitude - (new Vector3(rb.velocity.x, 0f, rb.velocity.z)).magnitude;

        /*
        float speedDiff = Mathf.Clamp((targetVel.magnitude - (new Vector3(rb.velocity.x, 0f, rb.velocity.z)).magnitude),
            -maxSpeedGrounded, maxSpeedGrounded);

        Debug.Log("Speed diff: " + speedDiff);

        //acceleration rate
        float accelRate = Mathf.Abs(targetVel.magnitude) > 0.05f ? acceleration : deceleration;

        //magnitude of the actual movement
        float movement = Mathf.Abs(speedDiff * accelRate);

        Debug.Log("Movement: " + movement);

        Debug.Log("Final force: " + movement * targetVel.normalized);*/

        //add the force in the direction of the target velocity
        rb.AddForce(targetVel);


        //TIMERS FOR JUMP BUFFER AND COYOTE TIME
        timeSinceGrounded -= Time.deltaTime;
        timeSinceJump -= Time.deltaTime;


        //UPDATING ISGROUNDED

        Collider[] colliders = Physics.OverlapBox(transform.position + centerYOffset * Vector3.up, new Vector3(boxWidth, boxHeight, boxDepth));

        //foreach(Collider col in colliders)
        //{
        //    Debug.Log(col.gameObject);
        //}

        if (colliders.Length > 1) //collider will always be in contact with player's collider, so we check if > 1 instead of 0
        {
            isGrounded = true;
            isJumping = false;
            timeSinceGrounded = coyoteTimeBuffer;
        } else
        {
            isGrounded = false;
        }

        if (!isJumping && timeSinceJump > 0 && timeSinceGrounded > 0)
        {
            Jump();
            isJumping = true;
        }

        //GRAVITY
        if (rb.velocity.y < 0)
        {
            rb.AddForce(new Vector3(0, -fallGravity, 0), ForceMode.Force);
        } else
        {
            rb.AddForce(new Vector3(0, -normalGravity, 0), ForceMode.Force);
        }

        //UPDATE ROTATION TO MATCH CAMERA LOOK
        

        

  

        //STOPPING FRICTION
        //TODO

    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0, jumpPower, 0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position + centerYOffset * Vector3.up, new Vector3(boxWidth, boxHeight, boxDepth));
    }
}

