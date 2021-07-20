using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crown : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D; 
    public Light lightFeature;

    private float _startPosition;
    private float _startRange;
    
    public string nextLevelName;
    
    public void loadLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPosition = transform.position.y;
        _startRange = lightFeature.range;
    }

    // Update is called once per frame
    void Update()
    {
        lightFeature.range = _startRange + 0.2f * Mathf.Cos(Time.time);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.position = new Vector2(_rigidbody2D.position.x, _startPosition + 0.3f * Mathf.Sin(Time.time));
    }
    
    /**
    * Input: hitBox
    * Purpose: Check if the player exits into any triggering hitBoxes   
    */
    private void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag($"Geo"))
        {
            loadLevel();
        }
    }
}
