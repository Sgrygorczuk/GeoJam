using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    public SpriteRenderer spriteRenderer;
    public Transform checkpoint;
    public Geo geo;
    public Color color;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = color;
    }
    
    /**
    * Input: hitBox
    * Purpose: Check if the player exits into any triggering hitBoxes   
    */
    private void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag($"Geo"))
        {
            geo.UpdateCheckpoint(checkpoint.position);
        }
    }
    
    
}
