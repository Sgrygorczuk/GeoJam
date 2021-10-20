using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public  Transform[] transformsOne;
    public  Transform[] transformsTwo;
    public  Transform[] transformsThree;
    public Transform cameraPosition;
    public float offset;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < transformsOne.Length; i++)
        {
            transformsOne[i].position =
               new Vector2(transformsOne[i].position.x, cameraPosition.position.y + 3.5f + offset * Mathf.Sin(i + Time.time) );
        }
        
        for (var i = 0; i < transformsTwo.Length; i++)
        {
            transformsTwo[i].position =
                new Vector2(transformsTwo[i].position.x, cameraPosition.position.y   + offset * Mathf.Sin(i + Time.time));
        }
        
        for (var i = 0; i < transformsThree.Length; i++)
        {
            transformsThree[i].position =
                new Vector2(transformsThree[i].position.x, cameraPosition.position.y - 3.5f  + offset * Mathf.Sin(i + Time.time));
        }
    }
}