using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour
{
    int GridSize_X, GridSize_Y;
    int[,] EventCounts; 
    public HeatMapCube HeatmapCubePrefab;

    public Gradient ColorGradient;

    public int MaxCounts=100;

    void CountEvent()
    {
        //for (int i = 0; i < CountEvents.Length; i++)
        //{
            //Find corresponding Gridcell
            //EventCounts[x,y]++:
        //}
    }

    void VisualizeEvents()
    {
        EventCounts = new int[GridSize_X, GridSize_Y];
        for (int i = 0; i < GridSize_X; i++)
        {
            for (int j = 0; j < GridSize_Y; j++)
            {
                //if event >0
                SpawnCube(i, j, EventCounts[i,j]);
            }
        }
    }

    private void SpawnCube(int x, int y, int counts)
    {
        Vector3 pos = new Vector3(x * GridSize_X, GetHeight(x * GridSize_X, y * GridSize_Y),  y * GridSize_Y);
        var cube = Instantiate(HeatmapCubePrefab, pos, Quaternion.identity) as HeatMapCube;
        float f = Mathf.Clamp01((float)counts / MaxCounts);
        Color c = ColorGradient.Evaluate(f);
        cube.SetColor(c);
    }

    private float GetHeight(int x, int y)
    {
         //Raycast
         Vector3 pos = new Vector3(x, 100, y);
         RaycastHit hit;
         if (Physics.Raycast(pos, Vector3.down, out hit))
         {
             return hit.point.y;
         }
         return 0;
    }
}
