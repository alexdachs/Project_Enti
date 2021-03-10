using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    private Rigidbody2D player;
    public GameObject checkpoint;

    public float cont = 0;
    public bool notMoving = false;

    public float dashDistance = 10.0f;
    private bool isDashing;
    public float dashCoolDown = 0.5f;
    private bool goingLeft = false;

    private const float stop = 0f;
    private const float move = 4.0f;
    public float speed = 4.0f;
    public float jumpPower = 10.0f;
    public int extraJumps = 1;
    public bool ground;

    private bool changeGravity = false;
    private float gravityForce = 5.0f;
    private bool stayTop = false;

    public bool isJumping = false;
    private bool isStacked;
    private float jumpCoolDown;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown ("Jump")) // Salto
        {
            Jump();        
        }

        if (Input.GetKeyDown(KeyCode.D)) // Dash
        {
            StartCoroutine(Dash(1f));
        }

        if (Input.GetKeyDown(KeyCode.W)) // Cambio Gravedad
        {
            GravityChange();
        }

        if (Input.GetKeyDown(KeyCode.S) && !isJumping) // Encogerse
        {
            player.transform.localScale = new Vector3(1, 0.65f, 1);
        }
        if (Input.GetKeyUp(KeyCode.S)) // Volver al tamaño normal
        {
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKeyUp(KeyCode.R)) // Reset
        {
            player.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, checkpoint.transform.position.z);
            player.gravityScale = gravityForce;
            changeGravity = false;
            stayTop = false;
            isJumping = false;
            notMoving = false;
            speed = move;
        }

       /* if (player.transform.position == player.transform.position)
        {
            cont = cont * Time.deltaTime;
        }*/

        //CheckGrounded();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            if (notMoving)
            {
                player.velocity = new Vector2(speed, player.velocity.y);
            }
            else
            {
                player.velocity = new Vector2(speed, player.velocity.y); // Movimiento constante
            }
        }   
    }

    void Jump()
    {
        if (!isJumping)
        {
            if (stayTop) // Salto estando en el techo
            {
                if (notMoving)
                {
                    if (ground)
                    {
                        isJumping = true;
                        if (isStacked) // Si esta stacked
                        {
                            player.velocity = new Vector2(player.velocity.x, -jumpPower);
                            cont = 0;
                        }
                        else
                        {
                            player.velocity = new Vector2(player.velocity.x, -jumpPower);
                        }
                    }
                    else
                    {
                        if (goingLeft) // Cambio de variable de dirección a la que vas (para el dash)
                        {
                            speed = -move;
                            goingLeft = false;
                        }
                        else
                        {
                            speed = move;
                            goingLeft = true;
                        }
                        speed = -1 * speed;
                        player.velocity = new Vector2(speed, player.velocity.y);
                        notMoving = false;
                        isJumping = true;
                        if (isStacked) // Si esta stacked
                        {
                            player.velocity = new Vector2(player.velocity.x, -jumpPower);
                            cont = 0;
                        }
                        else
                        {
                            player.velocity = new Vector2(player.velocity.x, -jumpPower);
                        }
                    }
                }
                else
                {
                    if (isStacked)
                    {
                        player.velocity = new Vector2(player.velocity.x, -jumpPower);
                        cont = 0;
                    }
                    else
                    {
                        player.velocity = new Vector2(player.velocity.x, -jumpPower);
                    }
                }
            }
            else // Salto normal
            {
                if (notMoving)
                {
                    if (ground)
                    {
                        isJumping = true;
                        if (isStacked)
                        {
                            player.velocity = new Vector2(player.velocity.x, jumpPower);
                            cont = 0;
                        }
                        else
                        {
                            player.velocity = new Vector2(player.velocity.x, jumpPower);
                        }
                    }
                    else
                    {
                        if (goingLeft) // Cambio de variable de dirección a la que vas (para el dash)
                        {
                            speed = -move;
                            goingLeft = false;
                        }
                        else
                        {
                            speed = move;
                            goingLeft = true;
                        }
                        speed = -1 * speed;
                        player.velocity = new Vector2(speed, player.velocity.y);
                        notMoving = false;
                        isJumping = true;
                        if (isStacked)
                        {
                            player.velocity = new Vector2(player.velocity.x, jumpPower);
                            cont = 0;
                        }
                        else
                        {
                            player.velocity = new Vector2(player.velocity.x, jumpPower);
                        }
                    }
                }
                else
                {
                    if (isStacked)
                    {
                        player.velocity = new Vector2(player.velocity.x, jumpPower);
                        cont = 0;
                    }
                    else
                    {
                        player.velocity = new Vector2(player.velocity.x, jumpPower);
                    }
                }
            }
        }
        else if (isJumping && notMoving)
        {
            if (stayTop)
            {
                if (goingLeft) // Cambio de variable de dirección a la que vas (para el dash)
                {
                    speed = -move;
                    goingLeft = false;
                }
                else
                {
                    speed = move;
                    goingLeft = true;
                }
                speed = -1 * speed;
                player.velocity = new Vector2(speed, player.velocity.y);
                notMoving = false;
                isJumping = true;
                if (isStacked) // Si esta stacked
                {
                    player.velocity = new Vector2(player.velocity.x, -jumpPower);
                    cont = 0;
                }
                else
                {
                    player.velocity = new Vector2(player.velocity.x, -jumpPower);
                }
            }
            else
            {
                if (goingLeft) // Cambio de variable de dirección a la que vas (para el dash)
                {
                    speed = -move;
                    goingLeft = false;
                }
                else
                {
                    speed = move;
                    goingLeft = true;
                }
                speed = -1 * speed;
                player.velocity = new Vector2(speed, player.velocity.y);
                notMoving = false;
                isJumping = true;
                if (isStacked)
                {
                    player.velocity = new Vector2(player.velocity.x, jumpPower);
                    cont = 0;
                }
                else
                {
                    player.velocity = new Vector2(player.velocity.x, jumpPower);
                }
            }
        }  
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor") // Reinicio del salto
        {
            isJumping = false;
            changeGravity = false;
        }
        if (col.gameObject.tag == "wall") //Rebote con la pared
        {
            notMoving = true;
            speed = stop;
            isJumping = false;
        }
        if (col.gameObject.tag == "floor")
        {
            ground = true;
        }
        /*if (col.gameObject.tag == "wall" && cont >= 2)
        {
            isStacked = true;
        }*/
        if (col.gameObject.tag == "trap") //Muerte por ''trampa'' y vuelta al inicio
        {
            player.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, checkpoint.transform.position.z);
            if (goingLeft)
            {
                speed = -1 * speed;
            }
            player.gravityScale = gravityForce;
            changeGravity = false;
            stayTop = false;
            isJumping = false;
            speed = move;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor") // Esta saltando (para no poder spamear el salto)
        {
            isJumping = true;
            ground = false;
        }
    }

   /* private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("wall") && cont >= 2000)
        {
            cont = 0;
            isStacked = true;
        }
    }*/


    IEnumerator Dash(float direction)
    {
        isDashing = true;
        player.velocity = new Vector2(player.velocity.x, 0f);

        if (!goingLeft) // Dash si vas hacia la derecha
        {
            player.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        }
        else // Dash si vas hacia la izquierda
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
        if (stayTop) // Cambio de variable de si estamos arriba o abajo para modificar el salto
        {
            stayTop = false;
        }
        else
        {
            stayTop = true;
        }
    }

}
