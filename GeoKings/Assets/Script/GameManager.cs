using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public string nextLevelName;
    
    public void loadLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
