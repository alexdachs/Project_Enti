using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    private Rigidbody2D player;
    public GameObject feet;
    public GameObject lateral_D;
    public GameObject lateral_I;


    public float dashDistance = 10.0f;
    private bool isDashing;
    public float dashCoolDown = 0.5f;
    private bool goingLeft = false;

    public float speed = 4.0f;
    public float jumpPower = 10.0f;
    public int extraJumps = 1;

    private bool changeGravity = false;
    private bool stayTop = false;

    private bool isJumping = false;
    private bool isGrounded;
    private float jumpCoolDown;
    private int jumpCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown ("Jump"))
        {
            Jump();        
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(Dash(1f));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GravityChange();
        }

        //CheckGrounded();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            player.velocity = new Vector2(speed, player.velocity.y); // Movimiento constante
        }
         
    }

    void Jump()
    {
        if (!isJumping)
        {
            if (stayTop)
            {
                player.velocity = new Vector2(player.velocity.x, -jumpPower);
            }
            else
            {
                player.velocity = new Vector2(player.velocity.x, jumpPower);
            } 
            isJumping = true;
        }  
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            isJumping = false;
            changeGravity = false;
        }
        if (col.gameObject.tag == "wall" && isJumping) 
        {
            speed = -1 * speed;
            isJumping = false;
            if (goingLeft)
            {
                goingLeft = false;
            }
            else
            {
                goingLeft = true;
            }

        }
    }


    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            isJumping = true;
        }
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        player.velocity = new Vector2(player.velocity.x, 0f);

        if (!goingLeft)
        {
            player.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        }
        else
        {
            player.AddForce(new Vector2(-dashDistance * direction, 0f), ForceMode2D.Impulse);
        }
        float gravity = player.gravityScale;
        player.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        player.gravityScale = gravity;
    }



    private void GravityChange()
    {
        if (!changeGravity)
        {
            player.gravityScale = -1 * player.gravityScale;
            changeGravity = true;
        }
        if (stayTop)
        {
            stayTop = false;
        }
        else
        {
            stayTop = true;
        }
    }


    /* void CheckGrounded() // Funcion check del salto (si toca el suelo puede saltar y resetea el contador)
     {
         if () // Check de los alrededores del jugador con el suelo
         {
             isGrounded = true;
             jumpCount = 0; 
             jumpCoolDown = Time.time + 0.2f;
         }
         else if (Time.time < jumpCoolDown) // Check del cooldown del salto
         {
             isGrounded = true;
         }
         else
         {
             isGrounded = false;
         }
     }*/
}
