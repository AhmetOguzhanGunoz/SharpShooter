using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playerControl : MonoBehaviour {

    public float speed = 3f;
    public bool ground = true;
    public bool dead = false;


    public Animator anim;

    public int maxHealth = 3;
    public int currentHealth;

    public int bulletDirect;
    public Quaternion bulletRotation;
    public int gunDirect;
    public Quaternion gunRotation;

    public int score;
    public Text scoreText;


    bool twiceJump = true;
    bool drawGun = false;

    float xScale = 0.3f;
    float yScale = 0.3f;

    float xSpawn = -5;
    float ySpawn = -2;
    float zSpawn = -0.1f;

    float atDeadTime = 0.7f;
    float atDead;

    Rigidbody2D rb;

    public Rigidbody2D bullet;
    public Rigidbody2D gun;
    public GameObject spawn_point_bullet;
    public GameObject spawn_point_gun;


    public GameObject[] healthImage;

    public GameObject player;

	// Use this for initialization
	void Start () {

        bulletDirect = 1;
        bulletRotation = bullet.transform.rotation;
        gunDirect = 1;
        gunRotation = gun.transform.rotation;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
	
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(score);

        scoreText.text = "Score : " + score.ToString();


        if (CrossPlatformInputManager.GetButtonDown("FireButton"))
        {
            if(!drawGun)
            {
                Rigidbody2D gunClone = Instantiate(gun, spawn_point_gun.transform.position, gunRotation) as Rigidbody2D;
                drawGun = true;
                Destroy(gunClone.gameObject, 0.5f);
            }

            Rigidbody2D bulletclone = Instantiate(bullet, spawn_point_bullet.transform.position, bulletRotation) as Rigidbody2D;

            bulletclone.velocity = bulletDirect * spawn_point_bullet.transform.right * 50f;

            Destroy(bulletclone.gameObject, 2f);
            drawGun = false;

        }


        float yatayKuvvet = CrossPlatformInputManager.GetAxis("Horizontal");

        anim.SetFloat("yatayhiz", Mathf.Abs(yatayKuvvet));
        
        if(dead && Time.time > atDead)
        {
            anim.SetBool("die", false);
            dead = false;
        }

        if (yatayKuvvet > 0)
        {
          
            transform.Translate(yatayKuvvet * speed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(xScale, yScale);
            bulletDirect = 1;
            bulletRotation = bullet.transform.rotation;
            gunDirect = 1;
            gunRotation = gun.transform.rotation;
        }

        if (yatayKuvvet < 0)
        {

            transform.Translate(-yatayKuvvet * speed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(-xScale, yScale);
            bulletDirect = -1;
            bulletRotation = Quaternion.AngleAxis(180, Vector3.up);
            gunDirect = -1;
            gunRotation = Quaternion.AngleAxis(180, Vector3.up);
        }

        if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
        {
            if (ground)
            {
                rb.AddForce(Vector2.up * 40f);
                anim.SetBool("ground", false);
                ground = false;
                twiceJump = true;
            }
            else
            {
                if (twiceJump)
                {
                    twiceJump = false;
                    anim.SetBool("ground", false);
                    rb.AddForce(Vector2.up * 40f);
                }
            }
        }	
	}

    void OnCollisionEnter2D(Collision2D Object)
    {
        if (Object.gameObject.tag == "barrier")
        {
            ground = true;
            anim.SetBool("ground", true);
        }

        if (Object.gameObject.tag == "kakatus")
        {
            if (!dead)
            {
                Invoke("healthFunc", 0.5f);

                anim.SetBool("die", true);

                atDead = Time.time + atDeadTime;

                dead = true;
            }            
        }

        if (Object.gameObject.tag == "deadZone")
        {
            if(!dead)
            {
                Invoke("healthFunc", 0.5f);

                anim.SetBool("die", true);

                atDead = Time.time + atDeadTime;

                dead = true;
            }
        }

        if (Object.gameObject.tag == "normal")
        {
            if (!dead)
            {
                Invoke("healthFunc", 0.5f);

                anim.SetBool("die", true);

                atDead = Time.time + atDeadTime;

                dead = true;
            }
        }

        if (Object.gameObject.tag == "king")
        {
            if (!dead)
            {
                Invoke("healthFunc", 0.5f);

                anim.SetBool("die", true);

                atDead = Time.time + atDeadTime;

                dead = true;
            }
        }

        if (Object.gameObject.tag == "bonus")
        {
            if (!dead)
            {
                Invoke("healthFunc", 0.5f);

                anim.SetBool("die", true);

                atDead = Time.time + atDeadTime;

                dead = true;
            }
        }

    }

    void OnCollisionExit2D(Collision2D Object)
    {
        if (Object.gameObject.tag == "barrier")
        {
            ground = false;
            anim.SetBool("ground", false);
        }
    }

    void OnCollisionStay2D(Collision2D Object)
    {
        if (Object.gameObject.tag == "barrier")
        {
            ground = true;
            anim.SetBool("ground", true);
        }

    }

    void healthFunc()
    {
        currentHealth = currentHealth - 1;

        for (int i = 0; i < maxHealth; i++)
        {
            healthImage[i].SetActive(false);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            healthImage[i].SetActive(true);
        }
        
        if (currentHealth >= 1)
        {
            player.transform.position = new Vector3(xSpawn, ySpawn, zSpawn);

        }

        if (currentHealth == 0)
        {
            SceneManager.LoadScene(2);
        }

    }
}
