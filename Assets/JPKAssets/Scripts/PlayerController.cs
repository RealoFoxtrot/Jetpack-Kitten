using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    protected Rigidbody2D catBody;
    protected float horizontalMove;
    protected Vector2 forceDirection; // velocity
    protected int jumpCount;

    protected float flipDirection;
    

    public bool canJump;
    public bool grounded = false;

    public float moveSpeed = 7f;
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
        flipDirection = Input.GetAxis("Turn");

        forceDirection.x = horizontalMove * moveSpeed; // Setting x movement force

        if (jumpCount < jumpMax)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (horizontalMove != 0) // applying force when input is activated
        {
            //catBody.AddRelativeForce(forceDirection);
            catBody.AddForce( forceDirection);
        }

        if (flipDirection != 0)

        {
            catBody.AddTorque(flipDirection * flipForce);
        }

        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            catBody.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount += 1;
        }

	}

    void OnCollisionStay2D ( Collision2D collision)
    {
        Collider2D collider = collision.collider;

        if (collider.tag == "Floor")
        {
           // grounded = true;
            jumpCount = 0;
        }
        else
        {
           // grounded = false;
        }

    }
}
