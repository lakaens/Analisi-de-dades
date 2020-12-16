using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writer : MonoBehaviour
{
    public static Writer Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static void Print(string s)
    {
        Instance._Print(s);  
    }

    private void _Print(string s)
    {
        //write csv
        //Writer sql
    }
}
