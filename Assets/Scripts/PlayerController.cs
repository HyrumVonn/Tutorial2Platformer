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
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        scoreText.text = score.ToString();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            score += 1;
            scoreText.text = score.ToString();
            Destroy(other.gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {            
            if(Input.GetKey(KeyCode.W))
            {
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float movementX = Input.GetAxis("Horizontal");
        rig.AddForce(new Vector2(movementX, 0f) * movementSpeed);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
