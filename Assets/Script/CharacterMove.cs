using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.AccessControl;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterMove : MonoBehaviour
{
    // Start is called before the first frame update

   // public InputActionAsset actionAsset;
    public float moveSpeed;
    private Animator anim;
    private SpriteRenderer sr;
    PlayerCombat playercombat;
    private Rigidbody2D rb2d;
    float moveHorizontal;
    private BoxCollider2D boxCollider;
    [SerializeField] private TrailRenderer tr;
    public bool facingRight;

    public Text king;
    public GameObject cýkýs;
    public GameObject toplnabilir;
    CharacterHealth characterHealth;
    

    [Header("Jump")]
    public float jumpForce;
    public bool isGrounded;
    public bool canDoubleJump;

    [Header("Cooldown")]
    public float cooldown = 0.2f; //animasyonun bekleme süresi
    private float NextPlayTime = 0f;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 150f;
    private float dashingTime = 0.7f;
    private float dashingCooldown = 0.2f;

    [Header("Dead")]
    public Slider slider;
    public float health;
    public bool dead = false;

    [Header("Attack")]
    public bool characterattack;
    public float charactertimer;


    public GameObject deadcanvas;

    [Header("SFX")]
    private AudioSource audioSource;
    public AudioClip swordsound;
    public AudioClip jumpsound;
  

    

    void Start()
    {
        moveSpeed = 5;
        moveHorizontal = Input.GetAxis("Horizontal");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        playercombat = GetComponent<PlayerCombat>();
        boxCollider = GetComponent<BoxCollider2D>();
        sr= GetComponent<SpriteRenderer>();
        charactertimer = 0.7f;
        audioSource = GetComponent<AudioSource>();
        characterHealth = GetComponent<CharacterHealth>();
        
        
          

    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        CharacterAnimation();
        CharacterAttack();
        CharacterRunAttack();
        CharacterJump();
        CharacterAttackSpacing();


        if (dead)
        {
            deadcanvas.SetActive(true);
        }


        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        CharacterRoll();

        if (slider.value == 0)
        {
            dead = true;
        }

       

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

        void CharacterMovement()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveHorizontal * moveSpeed, rb2d.velocity.y);
    }

    void CharacterAnimation()
    {
        if (moveHorizontal > 0)
        {
            anim.SetBool("isRunning", true);
        }
        if (moveHorizontal == 0)
        {
            anim.SetBool("isRunning", false);

        }
        if (moveHorizontal < 0)
        {
            anim.SetBool("isRunning", true);
        }
        if (facingRight == false && moveHorizontal > 0)
        {
            CharacterFlip();

        }
        if (facingRight == true && moveHorizontal < 0)
        {
            CharacterFlip();

        }

    }

    void CharacterFlip()
    {

        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void CharacterAttack()
    {
        if (Input.GetButtonDown("Fire1") && moveHorizontal == 0)
        {
          
            if (characterattack)
            {
                anim.SetTrigger("isAttack");
                playercombat.DamageEnemy();
                characterattack = false;
                audioSource.PlayOneShot(swordsound);
                

            }


        }
    }

    void CharacterRunAttack()
    {
        if (Input.GetButtonDown("Fire1") && moveHorizontal > 0 || Input.GetButtonDown("Fire1") && moveHorizontal < 0)
        {
            
            if (characterattack)
            {
                anim.SetTrigger("isRunAttack");
                playercombat.DamageEnemy();
                characterattack = false;
                audioSource.PlayOneShot(swordsound);

            }

        }
    }

    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
            

            if (isGrounded)
            {
                rb2d.velocity = Vector2.up * jumpForce;
                canDoubleJump = true;
             
                audioSource.PlayOneShot(jumpsound);
            }

            else if (canDoubleJump)
            {

                jumpForce = jumpForce / 1.5f;
                rb2d.velocity = Vector2.up * jumpForce;

                canDoubleJump = false;
                jumpForce = jumpForce * 1.5f;


            }

        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        anim.SetBool("isJumping", false);

        if (col.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }
        

    }

    void OnCollisionStay2D(Collision2D col)
    {
        anim.SetBool("isJumping", false);
        if (col.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        anim.SetBool("isJumping", true);
        if (col.gameObject.tag == "Grounded")
        {
            isGrounded = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "king")
        {
            king.gameObject.SetActive(true);
        }
        if(col.gameObject.tag == "topla")
        {
            Destroy(toplnabilir,0.3f);
            cýkýs.gameObject.SetActive(true);
        }
        if (col.gameObject.tag == "cýkýs")
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        if (col.gameObject.tag == "deadarea")
        {
            characterHealth.Dead();
        }
        if (col.gameObject.tag == "cýkýs1")
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "king")
        {
            king.gameObject.SetActive(false);
        }
    }


    void CharacterRoll()
    {
        anim.SetFloat("x", Math.Abs(Input.GetAxisRaw("Horizontal")));

        if (Input.GetKeyDown(KeyCode.D))
            
         if(sr.flipX)
                sr.flipX = false;
        
        if(Input.GetKeyDown(KeyCode.A))
            if(!sr.flipX)
                sr.flipX = false;

        if (Input.GetKeyDown(KeyCode.LeftShift)&&Time.time>NextPlayTime)
        {
            NextPlayTime= Time.time+cooldown;
            anim.SetTrigger("isRolling");
        }
             
        }
    

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

   public void EnableCollider()
    {
        boxCollider.enabled = false;
    }

    public void DisableCollider()
    {
        boxCollider.enabled=true;
    }
    public void GetDamage(float damage)
    {
        if (health - damage >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0f;

        }
        amIdead();
    }

    void amIdead()
    {
        if (health == 0)
        {
            dead = true;
        }

    }


    void CharacterAttackSpacing()
    {
        if (characterattack == false)
        {
            charactertimer-=Time.deltaTime;
        }
        if(charactertimer<0)
        {
            charactertimer = 0f;
        }
        if (charactertimer == 0f)
        {
            characterattack = true;
            charactertimer = 0.7f;
        }
    }

    void DeadEvent()
    {
        boxCollider.enabled=false;
        rb2d.bodyType=RigidbodyType2D.Static;
    }

    

}