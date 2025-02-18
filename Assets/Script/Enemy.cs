using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float damage;
    private void OnTriggerEnter2D(Collider2D col)

    {
        if (col.tag == "Player")
        {
            col.GetComponent<CharacterMove>().GetDamage(damage);
        }
    }
}
