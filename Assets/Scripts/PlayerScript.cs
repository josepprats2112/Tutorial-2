using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    private int stagecounter = 0;

    public Text lives;

    private int livesValue = 3;

    public GameObject wintext;

    public GameObject losetext;

    public SpriteRenderer spriteRenderer;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        wintext.SetActive(false);
        losetext.SetActive(false);
        lives.text = "lives " + livesValue.ToString();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (scoreValue == 4)
        {
            if (stagecounter == 2)
            {
                wintext.SetActive(true);
            }
        }
        if (livesValue == 0)
        {
            losetext.SetActive(true);
            Destroy(gameObject);
        }
    }
    
private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
        if (scoreValue == 4)
        {
            stagecounter += 1;
            if (stagecounter == 1) 
            {
                transform.position = new Vector2(52.0f, 0.5f);
                score.text = scoreValue.ToString();
                livesValue = 3;
                lives.text = livesValue.ToString();
                scoreValue = 0;
            }
           
        }

        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
    }
}