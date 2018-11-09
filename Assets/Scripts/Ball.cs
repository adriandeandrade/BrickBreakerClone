using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        GameObject paddle = other.gameObject.GetComponent<PaddleController>().gameObject;

        if(paddle)
        {
            
        }
    }
}
