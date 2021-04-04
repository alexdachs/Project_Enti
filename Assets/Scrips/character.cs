using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class character : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D playerBox;
    public GameObject checkpoint;
    private GameMaster gm;

    public float dashDistance = 200.0f;
    public bool isDashing;
    public bool isAttacking;
    public float dashCoolDown = 0.5f;
    private bool goingLeft = false;

    private const float stop = 0f;
    private const float move = 100.0f;
    public float speed = 100.0f;
    public float jumpPower = 200.0f;
    public bool ground;

    public bool changeGravity = false;
    private float gravityForce = 40.0f;
    public bool stayTop = false;


    public bool killed = false;
    public bool isJumping = true;
    private bool isStacked;
    private float jumpCoolDown;

    public bool collectable = false;
    private bool isinmortal = false;

    //Animaciones
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerBox = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
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
            StartCoroutine(HelpDash());
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
        if(Input.GetKeyUp(KeyCode.I)) //inmortal
        {
            isinmortal = true;
        }
    }

    private void FixedUpdate()
    {
        float delta = Time.deltaTime * 65;
        if (!isDashing)
        {
            player.velocity = new Vector2(speed * delta, player.velocity.y); // Movimiento constante
            anim.SetBool("move", true);
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
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        speed = move;
                        goingLeft = true;
                        GetComponent<SpriteRenderer>().flipX = true;
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
            ground = true;

        }
        if (col.gameObject.tag == "wall") //Rebote con la pared
        {
            isJumping = false;
            isDashing = false;
         /*   float center_x = (playerBox.bounds.min.x + playerBox.bounds.max.x) / 2;
            Vector2 centerPosition = new Vector2(center_x, playerBox.bounds.min.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up, 0.5f);
            if (checkRaycastWall(hits)) { ground = true; }*/

        }
        if (col.gameObject.tag == "trap") //Muerte por ''trampa'' y vuelta al inicio
        {
            ground = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (col.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Victory");
        }
        if (col.gameObject.tag == "level1")
        {
            gm.lastCheckPoint = new Vector3(-270, -143, 0);
            SceneManager.LoadScene("Level_1");
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor") // Si se pone en el Collision Stay puedes cambiar 2 veces la gravedad
        {
            changeGravity = false;
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
            if (isinmortal)
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
            if(isinmortal == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
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
            isinmortal = true;
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
            yield return new WaitForSeconds(0.6f);
            isAttacking = false;
            isinmortal = false;
            player.gravityScale = gravity;
        }
       /* if (isDashing && ground)
        {
            isDashing = false;
        }*/
    }

    IEnumerator HelpDash()
    {
        if (!isDashing && !stayTop)
        {
            player.velocity = new Vector2(player.velocity.x, 50f);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Dash(0.6f));
        }
        if (!isDashing && stayTop)
        {
            player.velocity = new Vector2(player.velocity.x, -50f);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Dash(0.6f));
        }
    }

    private bool checkRaycastWall(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "wall") { return true; }
            }
        }
        return false;
    }

    private void GravityChange()
    {
        if (!changeGravity)
        {
            player.gravityScale = -1 * player.gravityScale;
            changeGravity = true;
            if (stayTop)
            {
                GetComponent<SpriteRenderer>().flipY = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipY = true;
            }
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
