using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Crown : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D; 
    public Light lightFeature;

    private float _startPosition;
    private float _startRange;
    
    public string nextLevelName;
    public Animator animator;
    public Image image;
    private bool _moveWipe = false;
    public bool floating = true;

    public AudioSource winSFX;

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

        if (_moveWipe && image.rectTransform.position.x < 5)
        {
            image.rectTransform.position = new Vector3(image.rectTransform.position.x + 5,
                image.rectTransform.position.y, 0);
        }
    }

    private void FixedUpdate()
    {
        if(floating){_rigidbody2D.position = new Vector2(_rigidbody2D.position.x, _startPosition + 0.3f * Mathf.Sin(Time.time));}
    }
    
    /**
    * Input: hitBox
    * Purpose: Check if the player exits into any triggering hitBoxes   
    */
    private void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag($"Geo"))
        {
            hitBox.gameObject.GetComponent<Geo>().SetStopMovement();
            StartCoroutine(Wipe());
        }
    }

    private IEnumerator Wipe()
    {
        if(!winSFX.isPlaying){
            winSFX.Play(0);
        }
        animator.Play("LeaveWipe");
        yield return new WaitForSeconds(1);
        loadLevel();
    }
}
