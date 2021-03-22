using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class character : MonoBehaviour
{
    private Rigidbody2D player;
    public GameObject checkpoint;
    private GameMaster gm;

    public float dashDistance = 200.0f;
    private bool isDashing;
    private bool isAttacking;
    public float dashCoolDown = 0.5f;
    private bool goingLeft = false;

    private const float stop = 0f;
    private const float move = 100.0f;
    public float speed = 100.0f;
    public float jumpPower = 200.0f;
    public bool ground;

    private bool changeGravity = false;
    private float gravityForce = 40.0f;
    private bool stayTop = false;


    public bool killed = false;
    public bool isJumping = false;
    private bool isStacked;
    private float jumpCoolDown;

    public bool collectable = false;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPoint;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadScene("Test_lvl");
        }
    }

    private void FixedUpdate()
    {
        float delta = Time.deltaTime * 65;
        if (!isDashing)
        {
            player.velocity = new Vector2(speed * delta, player.velocity.y); // Movimiento constante
        }   
    }

    void Jump()
    {
        if (!isJumping)
        {
            if (stayTop) // Salto estando en el techo
            {
                if (ground || killed)
                {
                    isJumping = true;
                    player.velocity = new Vector2(player.velocity.x, -jumpPower);
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
                    isJumping = true;
                    player.velocity = new Vector2(player.velocity.x, -jumpPower);
                }
            }
            else // Salto normal
            {
                if (ground || killed)
                {
                    isJumping = true;
                    player.velocity = new Vector2(player.velocity.x, jumpPower);
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
                    isJumping = true;
                    player.velocity = new Vector2(player.velocity.x, jumpPower);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor") // Reinicio del salto
        {
            isDashing = false;
            isJumping = false;
            changeGravity = false;
            ground = true;
        }
        if (col.gameObject.tag == "wall") //Rebote con la pared
        {
            isJumping = false;
        }
        if (col.gameObject.tag == "trap") //Muerte por ''trampa'' y vuelta al inicio
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (col.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Victory");
        }

        // ENEMIGOS

        if (col.gameObject.tag == "heavy_enemy" || col.gameObject.tag == "soldier_enemy" || col.gameObject.tag == "dron_enemy")
        {
            if (isAttacking)
            {
                Destroy(col.gameObject);
                isDashing = false;
                isJumping = false;
                killed = true;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor") // Esta saltando (para no poder spamear el salto)
        {
            isJumping = true;
            ground = false;
            killed = false;
        }
        if (col.gameObject.tag == "wall")
        {
            isJumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "projectile")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //COLECCIONABLE
        if (col.gameObject.tag == "collectable")
        {
            collectable = true;
            Destroy(col.gameObject);
        }
    }

    IEnumerator Dash(float direction)
    {
        if (!isDashing)
        {
            isDashing = true;
            isAttacking = true;
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
            isAttacking = false;
            player.gravityScale = gravity;
        }
        if (isDashing && ground)
        {
            isDashing = false;
        }
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
