using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


public class playerControl : MonoBehaviour {

    public float speed = 3f;
    public bool ground = true;
    public Animator anim;
    bool twiceJump = true;

    float xScale = 0.3f;
    float yScale = 0.3f;
    Rigidbody2D rb;

    public int maxHealth = 3;
    public int currentHealth;
    public GameObject[] healthImage;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
	
	}
	
	// Update is called once per frame
	void Update () {

        float yatayKuvvet = Input.GetAxis("Horizontal");

        anim.SetFloat("yatayhiz", Mathf.Abs(yatayKuvvet));

        if (yatayKuvvet > 0)
        {
          
            transform.Translate(yatayKuvvet * speed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(xScale, yScale);

        }

        if (yatayKuvvet < 0)
        {

            transform.Translate(-yatayKuvvet * speed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(-xScale, yScale);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ground)
            {
                rb.AddForce(Vector2.up * 300f);
                twiceJump = false;
                anim.SetBool("ground", false);
                ground = false;
            }
            else
            {
                if (twiceJump)
                {
                    twiceJump = false;
                    rb.AddForce(Vector2.up * 300f);
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

    }

    void OnCollisionExit2D(Collision2D Object)
    {
        if (Object.gameObject.tag == "barrier")
        {
            ground = false;
            anim.SetBool("ground", false);
            Invoke("healthFunc", 2f);
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

        if (currentHealth == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
