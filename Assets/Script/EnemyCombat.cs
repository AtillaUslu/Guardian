using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform enemyattackPoint;
    public LayerMask playerLayers;

    public float enemyattackRange = 0.5f;
    public int enemyattackDamage = 10;



    public void DamagePlayer()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(enemyattackPoint.position, enemyattackRange, playerLayers);
        foreach (Collider2D enemy in hitEnemies)
        {

            enemy.GetComponent<CharacterHealth>().TakeDamage(enemyattackDamage);
        }
    }

    private void OnDrawGizmosSelected()

    {
        if (enemyattackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(enemyattackPoint.position, enemyattackRange);
    }

}
