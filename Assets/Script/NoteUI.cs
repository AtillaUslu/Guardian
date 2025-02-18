using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteUI : MonoBehaviour
{
    [SerializeField] private GameObject Notobject;
    [SerializeField] private TMP_Text text;
    public static NoteUI Istance;
    public bool isnoteopen;

   

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Notobject.SetActive(true);
            isnoteopen = true;
            Time.timeScale = 0;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&isnoteopen==true)
        {
            Notobject.SetActive(false);
            Time.timeScale = 1;
           Destroy(gameObject,0.5f);
            Cursor.visible = true;
            Cursor.lockState= CursorLockMode.Locked;
        }
    }


}
