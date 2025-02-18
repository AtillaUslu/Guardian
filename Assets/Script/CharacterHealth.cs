using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    [Header("EnemySpacing")]
    public bool enemyattack;
    public float enemytimer;


    [Header("Health")]
    public float healstone=0 ;
    public Text healthstoneText;

    public GameObject deadpanel;
    CharacterMove CharacterMove;
   

    public Animator anim;
    void Start()
    {
        currentHealth=maxHealth;
        enemytimer = 1f;
        anim = GetComponent<Animator>();
        healstone = 1;
       
      
        
        
    }
    void EnemyAttackSpacing()
    {
        if (enemyattack==false)
        {
        enemytimer-=Time.deltaTime;
        }
        if (enemytimer < 0)
        {
            enemytimer=0f;
        }
        if (enemytimer == 0)
        {
            enemyattack=true;
            enemytimer=0.87f;
        }

    } 


   
    
    public void TakeDamage(int damage)
    {
        if (enemyattack)
        {
            currentHealth -= 10;
            enemyattack = false;
            anim.SetTrigger("isHurt");
        }

     healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Dead();
        }
    }

    
    public void Update()
    {
        EnemyAttackSpacing();
        UseHealStone();
        healthstoneText.text = healstone.ToString();



    }


   public void Dead()
    {
        anim.SetBool("isDead", true);
        GetComponent<CharacterMove>().enabled = false;
       

        Invoke("deadtime",2f);

    }

    void deadtime()
    {
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    void UseHealStone()
    {
        if (Input.GetKeyDown(KeyCode.E) && healstone> 0 && currentHealth < 100)
        {
            anim.SetTrigger("heal");
            healstone -= 1;
            currentHealth += 50;
            
        }

        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HealStone")
        {
            healstone += 0.5f;
            Destroy(collision.gameObject);
        }
    }
}
