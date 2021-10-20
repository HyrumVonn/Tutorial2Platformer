using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rig;
    public float movementSpeed = 10;
    public float jumpForce = 10;
    int score = 0;
    public int lvl1PassAmount = 4;
    public int gameWinAmount = 8;

    public Text scoreText;
    public Text winText;
    public Text livesText;
    Animator anim;
    string currentAnimationState;

    public int lives = 3;

    bool movementEnabled = true;

    public Transform groundCheckCorner1;
    public Transform groundCheckCorner2;

    public Transform lvl2SendToLocation;

    public MusicThing musicPlayer;



    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        winText.text = "";
        anim = GetComponent<Animator>();
        SetText();
        musicPlayer.Play1();
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if(other.gameObject.tag == "Coin")
    //    {
    //        score += 1;
    //        scoreText.text = score.ToString();
    //        Destroy(other.gameObject);
    //    }
    //}

    //void OnCollisionStay2D(Collision2D other)
    //{
    //    if (movementEnabled)
    //    {
    //        if (other.gameObject.tag == "Ground")
    //        {
    //            if (Input.GetKey(KeyCode.W))
    //            {
    //                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    //            }
    //        }
    //    }
    //}

    bool IsOnGround()
    {
        Collider2D[] collArr = Physics2D.OverlapAreaAll(groundCheckCorner1.position, groundCheckCorner2.position);

        if (collArr.Length > 0)
        {
            for (int i = 0; i < collArr.Length; i++)
            {
                if (collArr[i].isTrigger)
                { }
                else
                {
                    return true;
                }
            }
        }

        return false;

    }

    public void ScoreIncrease(int amount)
    {
        score += 1;

        if(score == lvl1PassAmount)
        {
            transform.position = lvl2SendToLocation.position;
            lives = 3;
        }

        if(score >= gameWinAmount)
        {
            winText.text = "You winned!\nGame by Hyrum";

            musicPlayer.Play2();
        }
        SetText();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementEnabled)
        {
            float movementX = Input.GetAxis("Horizontal");

            rig.velocity = new Vector2(movementX * movementSpeed, rig.velocity.y);
        }

        //rig.AddForce(new Vector2(movementX, 0f) * movementSpeed);
    }

    public void TakeDamage(int damage)
    {
        lives = lives - damage;
        SetText();

        if (lives <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(rig);
            movementEnabled = false;
            winText.text = "Oh no, you losted.\nSo sad :{\nPress 'Esss-cap-eh' to quit";
            winText.color = new Color(255f, 255f, 255f);
        }
    }

    void SetText()
    {
        scoreText.text = "Win Points: " + score.ToString() + "/8";

        livesText.text = "Lives: " + lives.ToString();
    }

    void SetAnimation(string newState)
    {
        if(newState == currentAnimationState)
        {
            return;
        }

        if(IsOnGround())
        {
            anim.Play(newState);
        }
        else
        {
            if (rig.velocity.y > 0)
            {
 //               anim.Play("JumpUpward");
            }
            else
            {
                anim.Play("Fall");
                newState = "Fall";
            }
        }

        currentAnimationState = newState;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(IsOnGround())
            {
                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            }
            SetAnimation("Jump");
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }



        if (IsOnGround())
        {
            if (rig.velocity.x != 0)
            {

                SetAnimation("Run");

            }
            else
            {
                SetAnimation("Idle");
            }
        }
        else
        {
            if (rig.velocity.y < 0)
            {
                SetAnimation("Fall");
            }
        }

        if (rig.velocity.x * transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

    }
}
