using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notitem : MonoBehaviour    
{
    [SerializeField][TextArea] private string noteString;
   
    public  string GetNote()
    {
        return noteString;
    }

   
}
