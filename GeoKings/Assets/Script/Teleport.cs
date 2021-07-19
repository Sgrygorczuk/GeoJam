using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform exit;
    public Geo geo;
    
    /**
    * Input: hitBox
    * Purpose: Check if the player exits into any triggering hitBoxes   
    */
    private void OnTriggerExit2D(Collider2D hitBox)
    {
        print("hit");
        if (hitBox.CompareTag($"Geo"))
        {        print("Geo");
            var position = exit.position;
            geo.Teleport(position.x, position.y);
        }
    }

}
