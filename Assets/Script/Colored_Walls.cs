using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Colored_Walls : MonoBehaviour
{
    public GameObject[] coloredWalls;
    public Geo geo;

    void Update()
    {
        foreach (var coloredWall in coloredWalls)
        {
            coloredWall.GetComponent<BoxCollider2D>().enabled =
                coloredWall.transform.GetComponent<SpriteRenderer>().color != geo.spriteRenderer.color;
        }
    }
}
