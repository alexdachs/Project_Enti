using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class character : MonoBehaviour
{
    public AudioSource dashSound;
    private AudioSource runSound;
    private Rigidbody2D player;
    private BoxCollider2D playerBox;
    public GameObject checkpoint;
    public GameObject lvlManager;
    private GameMaster gm;

    public float dashDistance = 200.0f;
    public bool isDashing;
    public bool isAttacking;
    public float dashCoolDown = 0.5f;
    private bool goingLeft = false;
    private bool isDead = false;

    private const float stop = 0f;
    private const float move = 300.0f;
    public float speed = 300.0f;
    public float jumpPower = 200.0f;
    public bool ground;

    //Spawn lvl controll
    private int lvlNumber = 1;
    public string lvlString;
    private int lvlX = 6000;
    public bool firstSpawn;
    public bool spawn;
    public float currentTimelvl;
    private float timebtwLvls = 5.0f;
    //SpeedBoost
    private float speedBoost = 50.0f;
    private float speedBoostX = 12000;
    private bool speedBoostActivate;
    private float timebtwBoost = 5.0f;
    private float currentBoostTime;

    private float deadTime;
    private bool endLevel = false;

    public bool changeGravity = false;
    //private float gravityForce = 40.0f;
    public bool stayTop = false;


    public bool killed = false;
    public bool isJumping = true;
    private bool isStacked;
    private float jumpCoolDown;

    public float collectable;
    private bool isinmortal = false;

    //Animaciones
    private Animator anim;

    //Stopwatch
    float timer;
    float seconds;
    float minutes;
    float hours;

    [SerializeField] Text stopWatchText;

    //Guardados
    string filetimer;
    string savecollect;
    string savehighscore;
    //string savedeaths;
    

    //Level complete
    public GameObject levelcomplet;
    public GameObject starcomplete;
    public GameObject startime;
    public GameObject starcompletef;
    public GameObject startimef;
    public GameObject starcollect;
    public GameObject stopwatch;
    public Text timefinish;
    float timerfinish;
    float secondsf;
    float minutesf;
    float hoursf;
    public Text minimumtime;
    float timerminimum = 90f;
    float secondsm;
    float minutesm;
    float hoursm;
    public GameObject collectableyes;
    public GameObject collectableno;
    //public Text deathcount;
    //int deaths;






    // Start is called before the first frame update
    void Start()
    {
        timer = PlayerPrefs.GetFloat(filetimer, 0); //Stopwatch
        collectable = PlayerPrefs.GetFloat(savecollect, 0);
        player = GetComponent<Rigidbody2D>();
        playerBox = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPoint;
        runSound = GetComponent<AudioSource>();
        dashSound = GameObject.FindGameObjectWithTag("dashSound").GetComponent<AudioSource>();
        currentTimelvl = timebtwLvls;
        
    }

    // Update is called once per frame
    void Update()
    {
        stopWatchCalcul();

        //Level Manager
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Procedural"))
        {
            lvlString = lvlNumber.ToString();
            //Spawn lvls
            if (transform.position.x > 2000 && transform.position.x < 2200 && currentTimelvl <= 0)
            {
                firstSpawn = true;
                currentTimelvl = timebtwLvls;
                lvlNumber = Random.Range(1, 7);
                lvlString = lvlNumber.ToString();
            }
            else
            {
                currentTimelvl -= Time.deltaTime;
            }
            if (transform.position.x > lvlX && transform.position.x < lvlX + 200 && currentTimelvl <= 0)
            {
                spawn = true;
                currentTimelvl = timebtwLvls;
                lvlNumber = Random.Range(1, 7);
                lvlString = lvlNumber.ToString();
            }
            if (firstSpawn)
            {
                lvlManager.GetComponent<LevelManager>().instanciaPrefab(lvlString);
                firstSpawn = false;
            }
            if (spawn)
            {
                lvlManager.GetComponent<LevelManager>().instanciaPrefab(lvlString);
                lvlX += 4000;
                spawn = false;
            }
            if (transform.position.x > speedBoostX && transform.position.x < speedBoostX + 100 && currentBoostTime <= 0)
            {
                speedBoostActivate = true;
                currentBoostTime = timebtwBoost;
            }
            else
            {
                currentBoostTime -= Time.deltaTime;
            }
            if (speedBoostActivate)
            {
                speed += speedBoost;
                speedBoostX += speedBoostX;
                speedBoostActivate = false;
            }
        }

        //Player
        if (!ground && !endLevel && !isDead)
        {
            runSound.Play();
        }

        if (isDead)
        {
            deadTime += Time.deltaTime;

            if (deadTime >= 1.6f)
            {
                PlayerPrefs.SetFloat(filetimer, timer);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


            }
        }

        if (isJumping)
        {
            anim.SetBool("jump", true);
        }
        else if (!isJumping)
        {
            anim.SetBool("jump", false);
        }

        if (Input.GetButtonDown("Jump")) // Salto
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.D)) // Dash
        {
            StartCoroutine(HelpDash());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            runSound.Stop();
        }

        if (!stayTop)
        {
            if (Input.GetKeyDown(KeyCode.W)) // Cambio Gravedad
            {
                if (!isAttacking)
                {
                    GravityChange();
                }
            }

            if (Input.GetKeyDown(KeyCode.S) && !isJumping) // Encogerse
            {
                player.transform.localScale = new Vector3(1, 0.90f, 1);
                anim.SetBool("slide", true);
            }
            if (Input.GetKeyUp(KeyCode.S)) // Volver al tamaño normal
            {
                player.transform.localScale = new Vector3(1, 1, 1);
                anim.SetBool("slide", false);
            }
        }
        if (stayTop)
        {
            if (Input.GetKeyDown(KeyCode.S)) // Cambio Gravedad
            {
                if (!isAttacking)
                {
                    GravityChange();
                }
            }

            if (Input.GetKeyDown(KeyCode.W) && !isJumping) // Encogerse
            {
                player.transform.localScale = new Vector3(1, 0.90f, 1);
                anim.SetBool("slide", true);
            }
            if (Input.GetKeyUp(KeyCode.W)) // Volver al tamaño normal
            {
                player.transform.localScale = new Vector3(1, 1, 1);
                anim.SetBool("slide", false);
            }
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
        // if(Input.GetKeyDown(KeyCode.Escape)){
        //     SceneManager.LoadScene("Menu");
        // }
    }

    private void FixedUpdate()
    {
        float delta = Time.deltaTime * 65;
        if (!isDashing && !endLevel && !isDead)
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
                /*else
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
                    player.velocity = new Vector2(player.velocity.x, -jumpPower);
                }*/
            }
            else // Salto normal
            {
                if (ground || killed)
                {
                    isJumping = true;
                    player.velocity = new Vector2(player.velocity.x, jumpPower);
                }
               /* else
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
                }*/
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
            runSound.Stop();
            anim.SetBool("death", true);
            ground = true;
            isDead = true;
            //deaths =  +1;
            //PlayerPrefs.SetInt(savedeaths, deaths);
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
                runSound.Stop();
                anim.SetBool("death", true);
                isDead = true;
                // deaths =  +1;
                // PlayerPrefs.SetInt(savedeaths, deaths);
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
                runSound.Stop();
                anim.SetBool("death", true);
                isDead = true;
            }
        }

        //COLECCIONABLE
        if (col.gameObject.tag == "collectable")
        {
            collectable = 1;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "checkpoint") {
            PlayerPrefs.SetFloat(filetimer, timer);
            PlayerPrefs.SetFloat(savecollect, collectable);
        }
        if(col.gameObject.tag == "endlevel") {
            Time.timeScale = 0f;
            runSound.Stop();
            endLevel = true;
            PlayerPrefs.SetFloat(filetimer, timer);
            levelcomplet.SetActive(true);
            starcompletef.SetActive(false);
            stopwatch.SetActive(false);
            starcomplete.SetActive(true);
            //coger tiempo y mostrar/calcular
            timerfinish = PlayerPrefs.GetFloat(filetimer, 0);
            secondsf = (int)(timerfinish % 60);
            minutesf = (int)((timerfinish / 60) % 60);
            hoursf = (int)(timerfinish / 3600);
            timefinish.text = hoursf.ToString("00") + ":" + minutesf.ToString("00") + ":" + secondsf.ToString("00");

            secondsm = (int)(timerminimum % 60);
            minutesm = (int)((timerminimum / 60) % 60);
            hoursm = (int)(timerminimum / 3600);
            minimumtime.text = hoursm.ToString("00") + ":" + minutesm.ToString("00") + ":" + secondsm.ToString("00");

            //deathcount.text = deaths.ToString("00");

            //collectable = PlayerPrefs.GetInt(savecollect, 0);
            //deaths = PlayerPrefs.GetInt(savedeaths, 0);
            if( timerfinish <= timerminimum ) {
                startime.SetActive(true);
                startimef.SetActive(false);
            }
            if(collectable > 0) {
                starcollect.SetActive(true);
                collectableyes.SetActive(true);
                collectableno.SetActive(false);
            }
            PlayerPrefs.DeleteKey(filetimer);
            PlayerPrefs.DeleteKey(savecollect);
            //PlayerPrefs.DeleteKey(savedeaths);

        }
        //mensajes tutorial
        
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
            anim.SetBool("dash", true);
            player.velocity = new Vector2(player.velocity.x, 50f);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Dash(0.6f));
            anim.SetBool("dash", false);
        }
        if (!isDashing && stayTop)
        {
            anim.SetBool("dash", true);
            player.velocity = new Vector2(player.velocity.x, -50f);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Dash(0.6f));
            anim.SetBool("dash", false);
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
    void stopWatchCalcul() {
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        minutes = (int)((timer / 60) % 60);
        hours = (int)(timer / 3600);
        stopWatchText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");

    }

}
