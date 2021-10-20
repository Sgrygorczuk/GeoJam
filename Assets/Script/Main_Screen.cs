using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Screen : MonoBehaviour
{
    public Text text;
    private bool _goingDown = true;

    // Update is called once per frame
    public void Update()
    {
        
        if (_goingDown)
        {
            text.color = new Vector4(text.color.r, text.color.b, text.color.g, text.color.a - 0.01f);
            if (text.color.a <= 0)
            {
                _goingDown = false;
            }
        }
        else
        {
            text.color = new Vector4(text.color.r, text.color.b, text.color.g, text.color.a + 0.01f);
            if (text.color.a >= 1)
            {
                _goingDown = true;
            }
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
