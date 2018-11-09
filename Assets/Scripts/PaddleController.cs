using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Ball ball;

    private void Start()
    {
        
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        transform.Translate(new Vector3(x, 0f, 0));

        CheckBounds();
    }

    private void CheckBounds()
    {
        if(transform.position.x >= 12)
        {
            transform.position = new Vector3(12f, transform.position.y, 0f);
        }

        if (transform.position.x <= -12)
        {
            transform.position = new Vector3(-12f, transform.position.y, 0f);
        }
    }
}
