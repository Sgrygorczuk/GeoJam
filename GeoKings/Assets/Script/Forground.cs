using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forground : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Color color;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = color;
    }
}
