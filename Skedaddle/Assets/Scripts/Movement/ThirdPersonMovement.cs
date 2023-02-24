using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Rigidbody myRB;
    public Transform orientation;

    public float playerHeight;

    public float horizontalInput;
    public float verticalInput;

    public Vector3 direction;

    public float speed = 6f;

    //Ground
    public LayerMask Ground;
    public bool grounded;
    public float groundDrag;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        myRB.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        myRB.AddForce(direction.normalized * speed * 10f, ForceMode.Force);

        //A raycast to make sure the player is on the ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (grounded)
            myRB.drag = groundDrag;
        else
            myRB.drag = 0;

        Vector3 flatVel = new Vector3(myRB.velocity.x, 0f, myRB.velocity.z);
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            myRB.velocity = new Vector3(limitedVel.x, myRB.velocity.y, limitedVel.z);
        }

        //Vector3 tempVelocity = myRB.velocity;
        //tempVelocity.x = Input.GetAxisRaw("Horizontal") * speed;
        //tempVelocity.y = Input.GetAxisRaw("Vertical") * speed;

        //Vector3 velocity = (transform.forward * tempVelocity.y) + (transform.right * tempVelocity.x);
        //velocity.y = myRB.velocity.y;
        //myRB.velocity = velocity;
        //    float horizontal = Input.GetAxisRaw("Horizontal");
        //    float vertical = Input.GetAxisRaw("Vertical");
        //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //    if (direction.magnitude >= 0.1f)
        //    {
        //        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //        float angle = Mathf.SmoothDamp(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //        Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        //        controller.Move(moveDir.normalized * speed * Time.deltaTime);
        //    }
    }
}
