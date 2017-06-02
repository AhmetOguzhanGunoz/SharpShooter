using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class monsterAI : MonoBehaviour {

    public GameObject enemyGraphics;
    public playerControl character;

    //facing control
    bool canFlip = true;
    bool facingRight = false;
    float flipTime = 5f;
    float nextFlipTime;
    float nextFlipChange = 0f;


    //atacking control
    public float chargeTime;
    float startChargeTime;
    public float enemySpeed = 20f;

    //healthbar
    public GameObject healthBar;
    public float damage = 100f;
    public float kingDamage = 150f;
    public GameObject kingHealthBar;


    //rigidbody
    Rigidbody2D enemyRB;
    Animator enemyAnim;

    // Use this for initialization
    void Start()
    {
        character = GameObject.Find("player").GetComponent<playerControl>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnim = enemyGraphics.GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Time.time > nextFlipChange)
        {

            flipFacing();

            nextFlipChange = Time.time + flipTime;
        }

    }

    void flipFacing()
    {

        if (!canFlip)
            return;

        float facingX = enemyGraphics.transform.localScale.x;
        facingX *= -1f;

        enemyGraphics.transform.localScale = new Vector3(facingX, enemyGraphics.transform.localScale.y,enemyGraphics.transform.localScale.z);

        facingRight = !facingRight;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "shoot")
        {
            if(enemyGraphics.gameObject.tag == "50")
            {
                damage = damage - 25f;

                healthBar.transform.localScale = new Vector3(damage / 100, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

                Destroy(other.gameObject);

                if (damage == 0f)
                {
                    character.score = character.score + 50;

                    Destroy(enemyGraphics.gameObject);
                    Destroy(gameObject);

                }
            }
            if (enemyGraphics.gameObject.tag == "100")
            {
                damage = damage - 25f;

                healthBar.transform.localScale = new Vector3(damage / 100, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

                Destroy(other.gameObject);

                if (damage == 0f)
                {
                    character.score = character.score + 100;

                    Destroy(enemyGraphics.gameObject);
                    Destroy(gameObject);

                }
            }
            if (enemyGraphics.gameObject.tag == "200")
            {
                kingDamage = kingDamage - 25f;

                kingHealthBar.transform.localScale = new Vector3(kingDamage / 150, kingHealthBar.transform.localScale.y, kingHealthBar.transform.localScale.z);

                Destroy(other.gameObject);

                if (kingDamage == 0f)
                {
                    character.score = character.score + 200;

                    Destroy(enemyGraphics.gameObject);
                    Destroy(gameObject);
                    SceneManager.LoadScene(3);

                }
            }
        }

        if (other.gameObject.tag == "Player")
        {

            if (facingRight && other.transform.position.x < enemyGraphics.transform.position.x)
            {

                flipFacing();

            }
            else if (!facingRight && other.transform.position.x > enemyGraphics.transform.position.x)
            {

                flipFacing();
            }

            canFlip = false;

            startChargeTime = Time.time + 1f;

            enemyAnim.SetBool("charge", true);

        }
        

        if (other.gameObject.tag == "deadZone")
        {

            Destroy(enemyGraphics.gameObject);
            Destroy(gameObject);

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {

            if (startChargeTime >= Time.time)
            {

                if (!facingRight)
                {

                    enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);

                }
                else
                {

                    enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);

                }

            }
        }


    }

    void OnTriggerExit2D(Collider2D other)
    {

        canFlip = true;
        enemyRB.velocity = new Vector2(0f, 0f);
        enemyAnim.SetBool("charge", false);

    }
}
