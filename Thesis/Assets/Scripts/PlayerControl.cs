using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{   public GameObject groundCheck;
    public GameObject Player;
    public GameObject StartingPos;

    bool facingRight = true;
    private float horizontalInput;
    private Rigidbody2D _rigidbody;
    private float xMovement;
    public float PlayerSpeed = 6;
    public float JumpForce = 14;
    [SerializeField]bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HorizontalMove();
        Jump();
    }
    void HorizontalMove()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        xMovement = horizontalInput * PlayerSpeed;

            _rigidbody.velocity = new Vector2(xMovement, _rigidbody.velocity.y);
            if (horizontalInput > 0 && !facingRight)
            {
                Flip();
            }
            if (horizontalInput < 0 && facingRight)
            {
                Flip();
            }
    }
    void Jump()
    {
        if (grounded)
        {
            if (Input.GetKey("space") || Input.GetKey("w") || Input.GetKey("up"))
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }
        } 
    }
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Spike")
        {
            Player.transform.position = StartingPos.transform.position;
            _rigidbody.velocity = new Vector2(0, 0);

        }
        if (collision.gameObject.tag == "Coin")
        {
            //Destroy(collision.gameObject);
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {

            grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            grounded = false;
            
        }
        if (collision.gameObject.tag == "DeathZone")
        {
            Player.transform.position = StartingPos.transform.position;
            _rigidbody.velocity = new Vector2(0, 0);

        }


    }
}
