using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpawn : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Geo geo;
    public Color color;
    
    
    /**
    * Input: hitBox
    * Purpose: Check if the player exits into any triggering hitBoxes   
    */
    private void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag($"Geo"))
        {
            geo.UpdateColor(color);
        }
    }
    
}
