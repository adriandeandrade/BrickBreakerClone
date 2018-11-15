using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private AudioClip glassBreak;

    private Camera cam;
    private AudioSource audioSource;
    private Renderer rend;

    private enum BrickType { OneHit, TwoHit, Invincible };
    [SerializeField] private BrickType brickType;

    private GameObject powerupPrefab;

    [SerializeField] private bool isPowerup;

    private void Start()
    {
        cam = Camera.main;
        audioSource = cam.gameObject.GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        powerupPrefab = GameManager.instance.powerupPrefab;

        switch (brickType)
        {
            case BrickType.OneHit:
                rend.material.color = GameManager.instance.oneHitColor;
                break;

            case BrickType.TwoHit:
                rend.material.color = GameManager.instance.twoHitColor;
                break;

            case BrickType.Invincible:
                rend.material.color = GameManager.instance.invincibleColor;
                break;
        }

        int randomNum = Random.Range(1, 100);

        if(randomNum <= 10 && brickType != BrickType.Invincible)
        {
            isPowerup = true;
        } 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ball"))
        {
            switch (brickType)
            {
                case BrickType.Invincible:
                    break;

                case BrickType.OneHit:
                    cam.gameObject.GetComponent<CameraShake>().ShakeCamera();
                    audioSource.PlayOneShot(glassBreak);
                    GameManager.instance.bricksAmount--;

                    if(isPowerup)
                    {
                        SpawnPowerup();
                    }
                    
                    Destroy(gameObject);
                    break;

                case BrickType.TwoHit:
                    cam.gameObject.GetComponent<CameraShake>().ShakeCamera();
                    brickType = BrickType.OneHit;
                    rend.material.color = GameManager.instance.oneHitColor;
                    break;
            }
        }
    }

    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, transform.position, Quaternion.identity);
    }
}
