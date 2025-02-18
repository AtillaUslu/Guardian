using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private Animator anim;
    public int maxHealth = 100;
    int currentHealth;

    SkeletonAI skeletonAI;



    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        skeletonAI = GetComponent<SkeletonAI>();


    }

    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
       

    }
    void Die()
    {
        anim.SetBool("isSkeletonDead", true);

        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        skeletonAI.followspeed = 0;
        Destroy(gameObject,3f);

    }


    
}
