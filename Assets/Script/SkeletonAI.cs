using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    private Animator anim;

    [Header("Haraket")]
    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;

    [Header("Mesafe")]
    public float distance;

    private Transform target;
    public float followspeed;

    EnemyCombat enemyCombat;
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        enemyCombat = GetComponent<EnemyCombat>();
    }

    void Update()
    {
        SkeletonAi();
    }

    void SkeletonMove()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * leftrightspeed, 1.0f));

        if (transform.position.x > oldPosition)
        {
            transform.localRotation=Quaternion.Euler(0,180,0);
        }

        if (transform.position.x< oldPosition)
        {
            transform.localRotation= Quaternion.Euler(0,0,0);
        }
        oldPosition=transform.position.x;
    }

    void SkeletonAi()
    {
        RaycastHit2D hitEnemy=Physics2D.Raycast(transform.position,-transform.right,distance);

        if (hitEnemy.collider !=null)
        {
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
          
            SkeletonFollow();

            enemyCombat.DamagePlayer();
        }
        else
        {
            Debug.DrawLine(transform.position,transform.position-transform.right*distance,Color.green);
            anim.SetBool("skeletonattack", false);
            SkeletonMove();
        }
    }

     private void OnTriggerEnter2D(Collider2D collision)
      {
          if(collision.tag == "Player")
          {
              anim.SetBool("skeletonattack", true);
          }
          else
          {
              anim.SetBool("skeletonattack", false);
          }
      }

      private void OnTriggerExit2D(Collider2D collision)
      {
          if(collision.tag == "Player")
          {
              anim.SetBool("skeletonattack",false);
          }
      }

   

    void SkeletonFollow()
    {
        Vector3 targetPosition = new Vector3(target.position.x, gameObject.transform.position.y, target.position.x);
        transform.position=Vector2.MoveTowards(transform.position, targetPosition, followspeed*Time.deltaTime);

    }
}
