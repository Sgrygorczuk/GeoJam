using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    //========================= Text 
    public Text credits; //Credits text 
    public Text exitText; //Credits text 
    public Rigidbody2D Transform;
    private bool _goingDown = true;
    
    /**
    *Purpose: Check for user Key press or text being off the screen 
    */
    private void Update()
    {
        if(credits.transform.position.y > 10){
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) { return; }
            SceneManager.LoadScene("Start_Screen");
        }
        else
        {
            Transform.position = new Vector3(Transform.position.x, Transform.position.y + 0.01f, 0);
            credits.transform.position = new Vector2(credits.transform.position.x, credits.transform.position.y + 0.01f);
        }

        
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start_Screen");
        }
        
        if (_goingDown)
        {
            exitText.color = new Vector4(exitText.color.r, exitText.color.b, exitText.color.g, exitText.color.a - 0.01f);
            if (exitText.color.a <= 0)
            {
                _goingDown = false;
            }
        }
        else
        {
            exitText.color = new Vector4(exitText.color.r, exitText.color.b, exitText.color.g, exitText.color.a + 0.01f);
            if (exitText.color.a >= 1)
            {
                _goingDown = true;
            }
        }
    }
}
