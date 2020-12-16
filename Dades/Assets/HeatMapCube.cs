using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapCube : MonoBehaviour
{
   public void SetColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;
    }
}
