using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private Rigidbody rBody;

    private AudioSource audioSource;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.velocity = Vector3.down * 15f * Time.deltaTime;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            AddOneLife();
        }
    }

    private void AddOneLife()
    {
        GameManager.instance.lives += 1;
        audioSource.PlayOneShot(GameManager.instance.powerUpSound);
        Destroy(gameObject);
    }
}
