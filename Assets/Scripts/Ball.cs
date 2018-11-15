using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 600f;
    [SerializeField] private float ballSpeed = 200f;
    [SerializeField] private float minVelocity;

    [SerializeField] private GameObject hitParticle;
    [SerializeField] private AudioClip bounce;

    private AudioSource audioSource;
    private Rigidbody rBody;
    private bool inPlay;

    private Vector3 currentVelocity;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !GameManager.instance.gameStarted)
        {
            transform.parent = null;
            GameManager.instance.gameStarted = true;
            GameManager.instance.arrow.SetActive(false);
            rBody.isKinematic = false;
            rBody.AddForce(GameManager.instance.arrow.transform.up * initialVelocity, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rBody.velocity = ballSpeed * (rBody.velocity.normalized);
        currentVelocity = rBody.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 spawnPoint = other.collider.ClosestPoint(transform.position);
        GameObject ps = Instantiate(hitParticle, spawnPoint, Quaternion.identity);
        Destroy(ps, 3f);
        audioSource.PlayOneShot(bounce);

        //CalculateBounce(other.contacts[0].normal);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottom"))
        {
            // TODO: Spawn ball dead effect.
            GameManager.instance.gameStarted = false;
            GameManager.instance.ResetPlayer();
            Destroy(gameObject);
        }
    }

    private void CalculateBounce(Vector3 normal)
    {
        float speed = currentVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(currentVelocity.normalized, normal);
        //rBody.velocity = direction * Mathf.Max(speed, minVelocity);
        rBody.velocity = direction * initialVelocity;
    }
}
