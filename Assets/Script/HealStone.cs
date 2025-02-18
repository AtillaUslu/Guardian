using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStone : MonoBehaviour
{
   
    private float originalY;
    public float floatStrength = 1f; // Süzülme gücünü ayarlayabilirsiniz
    public float speed = 0.5f; // Süzülme hýzýný ayarlayabilirsiniz

    void Start()
    {
        originalY = transform.position.y; // Baþlangýç y pozisyonunu kaydet
    }

    void Update()
    {
        // Sinüs fonksiyonu kullanarak y pozisyonunu güncelle
        transform.position = new Vector2(transform.position.x,
            originalY + Mathf.Sin(Time.time * speed) * floatStrength);
    }
}

