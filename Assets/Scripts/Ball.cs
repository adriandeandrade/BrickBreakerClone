using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialVelocity = 600f;
    [SerializeField] private float ballSpeed = 200f;
    [SerializeField] private float minVelocity;

    [SerializeField] private GameObject hitParticle;
    [SerializeField] private AudioClip bounce;

    private AudioSource audioSource;
    [HideInInspector] public Rigidbody rBody;
    private bool inPlay;

    public bool isSecondBall;

    private Vector3 currentVelocity;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        isSecondBall = false;
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

        if(other.CompareTag("Bottom") && isSecondBall)
        {
            Destroy(gameObject);
        }
    }
}
