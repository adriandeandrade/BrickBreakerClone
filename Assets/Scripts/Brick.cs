using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Camera cam;
    private AudioSource audioSource;
    [SerializeField] private AudioClip glassBreak;

    private void Start()
    {
        cam = Camera.main;
        audioSource = cam.gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Ball"))
        {
            cam.gameObject.GetComponent<CameraShake>().ShakeCamera();
            audioSource.PlayOneShot(glassBreak);
            Destroy(gameObject);
        }
    }
}
