using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    private Rigidbody2D player;
    public float speed = 4.0f;
    public float jumpPower = 10.0f;
    public int extraJumps = 1;

   
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

        //CheckGrounded();
    }

    private void FixedUpdate()
    {
        player.velocity = new Vector2(speed, player.velocity.y); // Movimiento constante
    }

    void Jump()
    {
        if (isGrounded || jumpCount < extraJumps)
        {
            player.velocity = new Vector2(player.velocity.x, jumpPower);
            jumpCount++;
        }  
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("floor"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("floor"))
        {
            isGrounded = false;
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
