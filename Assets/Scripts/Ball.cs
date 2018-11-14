using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 600f;

    [SerializeField] private GameObject hitParticle;
    [SerializeField] private AudioClip bounce;

    private AudioSource audioSource;
    private Rigidbody rBody;
    private bool inPlay;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !inPlay)
        {
            transform.parent = null;
            inPlay = true;
            rBody.isKinematic = false;
            rBody.AddForce(new Vector3(initialVelocity, initialVelocity, 0));
        }

        print(rBody.velocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 spawnPoint = other.collider.ClosestPoint(transform.position);
        GameObject ps = Instantiate(hitParticle, spawnPoint, Quaternion.identity);
        Destroy(ps, 3f);
        audioSource.PlayOneShot(bounce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottom"))
        {
            // TODO: Spawn ball dead effect.
            inPlay = false;
            GameManager.instance.ResetPlayer();
            Destroy(gameObject);
        }
    }
}
