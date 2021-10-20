using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform exit;
    public Geo geo;
    public bool isInOne;
    public bool isVertical;
    
    /**
    * Input: hitBox
    * Purpose: Check if the player exits into any triggering hitBoxes   
    */
    private void OnTriggerExit2D(Collider2D hitBox)
    {
        var position = exit.position;
        
        if (hitBox.CompareTag($"Geo"))
        {
            if (isVertical)
            {
                if ((isInOne && geo.GetVelocity().y < 0) || (!isInOne && geo.GetVelocity().y > 0))
                {
                    geo.Teleport(position.x, position.y);
                }
            }
            else
            {
                if ((isInOne && geo.GetVelocity().x > 0) || (!isInOne && geo.GetVelocity().x < 0))
                {
                    geo.Teleport(position.x, position.y);
                }
            }
        }
    }

}
