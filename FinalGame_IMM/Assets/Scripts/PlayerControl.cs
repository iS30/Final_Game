using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private Rigidbody playerRb;
    public GameManager gameManager;
    private Animator playerAnimator;
    public ParticleSystem explosionParticle;

    public float jumpForce;
    public float gravityModifier;
    public float zRange = 9;

    public AudioClip gemCollected;
    public AudioClip obstacleCollided;
    public AudioClip jumpSound;
    public AudioSource playerAudio;

    public bool isOnGround = true;
    public bool gameOver = false;
    public int pointValue;

    // Start is called before the first frame update
    void Start() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {       
        //make player jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        //keep player from falling from the sides
        if (transform.position.z < -zRange) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.z > zRange) {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        isOnGround = true;
    }

    //Collision Detection
    public void OnTriggerEnter(Collider Collectable) {
        if (Collectable.gameObject.CompareTag("Gem")) {
            playerAudio.PlayOneShot(gemCollected, 1.0f);
            Destroy(Collectable.gameObject);
            gameManager.UpdateScore(pointValue);
        }

        else if (Collectable.gameObject.CompareTag("Box")) {
            playerAudio.PlayOneShot(obstacleCollided, 1.0f);
            explosionParticle.Play();
            StartCoroutine(TriggerDeath());
        }
    }

    //Make death animation
    public IEnumerator TriggerDeath() {
        playerAnimator.SetBool("Death_b", true);
        playerAnimator.SetInteger("DeathType_int", 1);
        gameOver = true;
        yield return new WaitForSeconds(2.4f);
        gameManager.GameOver();
    }
}
