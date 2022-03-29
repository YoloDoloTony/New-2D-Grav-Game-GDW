using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public LayerMask ObjectLayer;

    //Player Movement
    public float playerSpeed;
    bool facingRight = true;

    Vector2 movementDir = new Vector2(0.0f, 0.0f);

    public float dashForce;
    public float delayTime;
    private float save;
    Vector2 currentPos;

    Animator animator;

    //Gravity Variables
    bool top;
    bool isGrounded;
    bool isVertical;
    bool isRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        save = delayTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, movementDir * dashForce, Color.green);

        Debug.Log("able to dash is " + AbleToDash());

        MovePlayer();

        //Check for Switch Gravity Input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            SwitchGravity();
            animator.SetBool("IsFlip", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && AbleToDash())
        {
            Debug.Log("pressed shift");
            Dash();
        }
    }

    //Player Movement
    private void MovePlayer()
    {
        updateScammerValue();
        movementDir = new Vector2(0f, 0f);

        //Horizontal player movement
        if (!isVertical)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movementDir += new Vector2(-1.0f, 0.0f);

                if (facingRight)
                {
                    FaceDirection();
                }
                animator.SetBool("IsWalking",true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movementDir += new Vector2(1.0f, 0.0f);

                if (!facingRight)
                {
                    FaceDirection();
                }
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }
        //Vertical (right) player movement
        if (isVertical && isRight)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movementDir += new Vector2(0.0f, -1.0f);

                if (facingRight)
                {
                    FaceDirection();
                }
                animator.SetBool("IsWalking", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movementDir += new Vector2(0.0f, 1.0f);

                if (!facingRight)
                {
                    FaceDirection();
                }
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }
        //Vertical (left) player movement
        if (isVertical && !isRight)
        {
            if (Input.GetKey(KeyCode.D))
            {
                movementDir += new Vector2(0.0f, 1.0f);

                if (facingRight)
                {
                    FaceDirection();
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movementDir += new Vector2(0.0f, -1.0f);

                if (!facingRight)
                {
                    FaceDirection();
                }
            }

        }
        rb.velocity = movementDir * (playerSpeed);
    }

    //Switches what way player is facing
    public void FaceDirection()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //180 degree Gravity Switch
    void SwitchGravity()
    {
        rb.gravityScale *= -1;
        Flip();

        isGrounded = false;
    }

    //Flip Player when they are on the roof
    void Flip()
    {
        //Flip for horizontal
        if (!isVertical)
        {
            if (!top)
            {
                transform.eulerAngles = new Vector3(0, 0, 180f);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
        //Flip for vertical (right)
        if (isVertical && isRight)
        {
            if (!top)
            {
                transform.eulerAngles = new Vector3(0, 0, -90f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 90f);
            }
        }
        //Flip for vertical (left)
        if (isVertical && !isRight)
        {
            if (!top)
            {
                transform.eulerAngles = new Vector3(0, 0, 90f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, -90f);
            }
        }

        facingRight = !facingRight;
        top = !top;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;

        animator.SetBool("IsFlip", false);

        if (collision.gameObject.CompareTag("platform"))
        {
            transform.parent = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            transform.parent = null;
        }
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsVertical()
    {
        return isVertical;
    }

    public void SetIsVertical(bool vertical)
    {
        isVertical = vertical;
    }

    public bool GetIsRight()
    {
        return isRight;
    }

    public void SetIsRight(bool right)
    {
        isRight = right;
    }

    public void Dash()
    {
        Debug.Log("dash");
        currentPos += movementDir * dashForce;
        transform.position = currentPos;
        ResetTimer();
    }

    // checks if the delay is up or not
    public bool CanDash()
    {
        if (delayTime - Time.realtimeSinceStartup < 0)
        {
            if (AbleToDash())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            delayTime -= Time.deltaTime;
            return false;
        }
    }

    //Reset delay timer 
    public void ResetTimer()
    {
        delayTime += Time.realtimeSinceStartup + save;
    }

    private bool AbleToDash()
    {
        if (facingRight)
        {
            return Physics2D.Raycast(rb.transform.position, Vector2.right, dashForce, ObjectLayer).collider == null;
        }
        else
        {
            return Physics2D.Raycast(rb.transform.position, Vector2.right * -1, dashForce, ObjectLayer).collider == null;
        }
    }

    private void updateScammerValue()
    {
        currentPos = new Vector2(transform.position.x, transform.position.y);
    }
}