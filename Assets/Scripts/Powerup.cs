using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerUpType { OneUp, SecondBall };
    public PowerUpType powerUpType;

    private AudioSource audioSource;
    private Rigidbody rBody;

    private void Awake()
    {
        ChooseType();
    }

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
            if (powerUpType == PowerUpType.OneUp)
            {
                AddOneLife();
            }
            else if (powerUpType == PowerUpType.SecondBall && !GameManager.instance.secondBall)
            {
                SpawnSecondBall(other.transform);
            }
        }
    }

    private void AddOneLife()
    {
        GameManager.instance.lives += 1;
        audioSource.PlayOneShot(GameManager.instance.powerUpSound);
        Destroy(gameObject);
    }

    private void SpawnSecondBall(Transform otherTransform)
    {
        GameObject newBall = Instantiate(GameManager.instance.ballPrefab, otherTransform.position + Vector3.up, Quaternion.identity);
        Ball ball = newBall.GetComponent<Ball>();
        ball.isSecondBall = true;
        ball.rBody.AddForce(-ball.initialVelocity, ball.initialVelocity, 0f, ForceMode.Impulse);
        GameManager.instance.secondBall = true;
    }

    public void ChooseType()
    {
        int randomNum = Random.Range(0, 2);

        switch (randomNum)
        {
            case 0:
                powerUpType = PowerUpType.OneUp;
                break;
            case 1:
                powerUpType = PowerUpType.SecondBall;
                break;
        }
    }
}
