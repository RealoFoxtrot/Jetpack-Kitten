using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    protected Rigidbody2D catBody;
    protected float horizontalMove;
    protected float verticalMove;
    protected Vector2 forceDirection; // velocity
    protected Vector2 aimDirection; //direction the cat is pointing, jump direction
    protected float aimAngle;
    protected Quaternion aimRotation;
    protected Quaternion currentRotation;

    

    public bool canJump;
    public bool grounded = false;
    protected int jumpCount;

    public float moveSpeed = 7f;
    public float turnSpeed = 0.3f;
    public float flipForce = 7f;
    public float jumpForce = 7f;
    public int jumpMax = 1;

    

	// Use this for initialization
	void Start ()
    {
        catBody = GetComponent<Rigidbody2D> (); //catBody is asigned to the connected ridigbody
        jumpCount = 0;

    }


    void FixedUpdate()
    {
        horizontalMove = Input.GetAxis("Horizontal"); // x axis input
        verticalMove = Input.GetAxis("Vertical"); // y axis input
        float TurnV = Input.GetAxis("TurnV"); // right stick x axis
        float TurnH = Input.GetAxis("TurnH"); // right stick y axis


        aimDirection = new Vector2(TurnH, TurnV);
        


        if (jumpCount < jumpMax)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (horizontalMove != 0 && grounded == true) // applying force when input is activated
        {
            forceDirection.x = horizontalMove * moveSpeed *-1; // Setting x movement force
            catBody.AddForce( forceDirection);
        }
        if (grounded == false)
        {
            if (TurnV != 0 && TurnH != 0)

            {
                aimAngle = Mathf.Atan2(TurnH, TurnV) * Mathf.Rad2Deg;
                aimRotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
                catBody.transform.rotation = Quaternion.Slerp(catBody.transform.rotation, aimRotation, turnSpeed * Time.time);
            }
        }

        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            catBody.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount += 1;
        }

        if (GetComponentInChildren<GroundedScript>().grounded == true)
        {
            jumpCount = 0;
            grounded = true;
        }
        else if (GetComponentInChildren<GroundedScript>().grounded == false)
        {
            grounded = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
	}

}
