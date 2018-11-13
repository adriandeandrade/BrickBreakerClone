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
        Movement();
    }

    private void Movement()
    {
        float xPos = transform.position.x + -(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
        Vector3 targetPos = new Vector3(Mathf.Clamp(xPos, -12f, 12f), 2f, 0f);
        transform.position = targetPos;
    }
}
