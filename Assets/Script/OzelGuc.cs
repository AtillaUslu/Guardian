using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OzelGuc : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public bool isCooldown = false;
    public float cooldownDuration = 30f;
    private float cooldownTimer = 0f;
    public Text cooldowntext;

    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            Debug.Log($"Cooldown devam ediyor. Kalan s�re: {cooldownTimer}");

            if (cooldownTimer <= 0f)
            {
                isCooldown = false; // Cooldown s�resi doldu
                cooldownTimer = 0f;
                Debug.Log("Cooldown bitti");
                // Cooldown bitti�inde yap�lacak i�lemler buraya eklenebilir
            }
        }

        // F tu�una bas�ld���nda ve cooldown aktif de�ilse �zel g�c� aktive et
        if (Input.GetKeyDown(KeyCode.F) && !isCooldown)
        {
            ActivateSpecialPower();
            StartCooldown(); // Cooldown ba�lat
        }
        cooldowntext.text=cooldownTimer.ToString();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component bulunamad�!");
        }
    }

    void ActivateSpecialPower()
    {
        // �zel g�� animasyonunu ba�lat
        animator.SetTrigger("specialPower");

        // Sa� ve sol taraftaki d��manlar� tespit et
        Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(transform.position + transform.right * attackRange, attackRange, enemyLayers);
        Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(transform.position - transform.right * attackRange, attackRange, enemyLayers);

        // Hasar verme i�lemi
        foreach (Collider2D enemy in hitEnemiesRight)
        {
            // Sa� taraftaki d��manlara hasar ver
            Skeleton skeleton = enemy.GetComponent<Skeleton>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(100); // �rnek hasar de�eri
            }
        }

        foreach (Collider2D enemy in hitEnemiesLeft)
        {
            // Sol taraftaki d��manlara hasar ver
            Skeleton skeleton = enemy.GetComponent<Skeleton>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(100); // �rnek hasar de�eri
            }
        }
    }

    void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownDuration; // Cooldown s�resini belirle
        Debug.Log($"Cooldown ba�lad�. S�re: {cooldownDuration} saniye.");
    }

    // Gizli olarak Unity edit�r�nde sald�r� menzilini g�stermek i�in
    void OnDrawGizmosSelected()
    {
        if (transform.right == null)
            return;

        Gizmos.DrawWireSphere(transform.position + transform.right * attackRange, attackRange);
        Gizmos.DrawWireSphere(transform.position - transform.right * attackRange, attackRange);
    }
}
