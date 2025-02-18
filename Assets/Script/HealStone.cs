using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStone : MonoBehaviour
{
   
    private float originalY;
    public float floatStrength = 1f; // S�z�lme g�c�n� ayarlayabilirsiniz
    public float speed = 0.5f; // S�z�lme h�z�n� ayarlayabilirsiniz

    void Start()
    {
        originalY = transform.position.y; // Ba�lang�� y pozisyonunu kaydet
    }

    void Update()
    {
        // Sin�s fonksiyonu kullanarak y pozisyonunu g�ncelle
        transform.position = new Vector2(transform.position.x,
            originalY + Mathf.Sin(Time.time * speed) * floatStrength);
    }
}

