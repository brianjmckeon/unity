using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public ParticleSystem explosionParticle;
    public ParticleSystem splatterParticle;
    public bool gameOver;
    public float jumpForce = 20f;
    public float gravityModifier = 5f;
    private bool isOnGround = true;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private bool hasDoubleJumped = false;
    private MoveLeft moveLeftScript;
    private int score = 0;
    private float startX = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        moveLeftScript = GameObject.Find("Background").GetComponent<MoveLeft>();
        InvokeRepeating("UpdateScore", 0, 1);
    }

    void UpdateScore()
    {
        // Increase the player's score
        if (!gameOver)
        {
            score += (int)System.Math.Pow(10, moveLeftScript.GetSpeedFactor());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            // Destroy all remaining obstacles
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Obstacle"))
            {
                Destroy(go);
            }
            return;
        }

        // If the game just started, have the player walk to the starting position
        if (transform.position.x < startX)
        {
            transform.Translate(Vector3.forward * 2f * Time.deltaTime);
        }

        // Display score
        Debug.Log("Score: " + score);

        // Toggle super speed on/off
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveLeftScript.ToggleSpeedFactor();
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool willJump = false;

            if (isOnGround)
            {
                hasDoubleJumped = false;
                willJump = true;
            }
            else // in the air...
            {
                // We can only jump once while in the air
                if (!hasDoubleJumped)
                {
                    willJump = true;
                    hasDoubleJumped = true;
                }
            }

            if (willJump)
            {
                splatterParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameOver) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            splatterParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            splatterParticle.Stop();
            gameOver = true;
            Debug.Log("Game Over! Final score: " + score);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            GameObject.Destroy(collision.gameObject);
        }
    }
}
