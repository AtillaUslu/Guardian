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
            Debug.Log($"Cooldown devam ediyor. Kalan süre: {cooldownTimer}");

            if (cooldownTimer <= 0f)
            {
                isCooldown = false; // Cooldown süresi doldu
                cooldownTimer = 0f;
                Debug.Log("Cooldown bitti");
                // Cooldown bittiðinde yapýlacak iþlemler buraya eklenebilir
            }
        }

        // F tuþuna basýldýðýnda ve cooldown aktif deðilse özel gücü aktive et
        if (Input.GetKeyDown(KeyCode.F) && !isCooldown)
        {
            ActivateSpecialPower();
            StartCooldown(); // Cooldown baþlat
        }
        cooldowntext.text=cooldownTimer.ToString();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component bulunamadý!");
        }
    }

    void ActivateSpecialPower()
    {
        // Özel güç animasyonunu baþlat
        animator.SetTrigger("specialPower");

        // Sað ve sol taraftaki düþmanlarý tespit et
        Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(transform.position + transform.right * attackRange, attackRange, enemyLayers);
        Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(transform.position - transform.right * attackRange, attackRange, enemyLayers);

        // Hasar verme iþlemi
        foreach (Collider2D enemy in hitEnemiesRight)
        {
            // Sað taraftaki düþmanlara hasar ver
            Skeleton skeleton = enemy.GetComponent<Skeleton>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(100); // Örnek hasar deðeri
            }
        }

        foreach (Collider2D enemy in hitEnemiesLeft)
        {
            // Sol taraftaki düþmanlara hasar ver
            Skeleton skeleton = enemy.GetComponent<Skeleton>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(100); // Örnek hasar deðeri
            }
        }
    }

    void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownDuration; // Cooldown süresini belirle
        Debug.Log($"Cooldown baþladý. Süre: {cooldownDuration} saniye.");
    }

    // Gizli olarak Unity editöründe saldýrý menzilini göstermek için
    void OnDrawGizmosSelected()
    {
        if (transform.right == null)
            return;

        Gizmos.DrawWireSphere(transform.position + transform.right * attackRange, attackRange);
        Gizmos.DrawWireSphere(transform.position - transform.right * attackRange, attackRange);
    }
}
