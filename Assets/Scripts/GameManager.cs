using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }

    }

    #endregion

    public int bricksAmount;
    public int lives;

    [SerializeField] private GameObject paddleWithBallPrefab;

    public GameObject arrow;
    public GameObject powerupPrefab;
    public GameObject ballPrefab;
    private GameObject[] bricks;

    public Color oneHitColor;
    public Color twoHitColor;
    public Color invincibleColor;

    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip powerUpSound;

    private UIManager ui;
    private Ball ball;
    private AudioSource audioSource;

    [HideInInspector] public bool gameStarted;
    [HideInInspector] public bool secondBall;

    private void Start()
    {
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        bricksAmount = bricks.Length;
        ui = GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();
        gameStarted = false;
        secondBall = false;

        if (ball == null)
        {
            ball = FindObjectOfType<Ball>();
        }
    }

    private void Update()
    {
        ui.bricksRemainingWorldText.text = "BRICKS: " + bricksAmount.ToString();
        ui.livesText.text = "LIVES: " + lives.ToString();

        if (bricksAmount <= 0)
        {
            ui.gameOverUI.SetActive(true);
            ui.gameOverText.text = "YOU WIN!";
            gameStarted = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (ball != null)
            {
                audioSource.PlayOneShot(winSound);
                Destroy(ball.gameObject);
            }
        }

        // Debug
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ResetPlayer()
    {
        if (lives > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Paddle"));
            lives -= 1;
            ui.livesText.text = "LIVES: " + lives.ToString();
            GameObject paddle = Instantiate(paddleWithBallPrefab, new Vector3(0f, 2f, 0f), Quaternion.identity);
            ball = paddle.GetComponentInChildren<Ball>();
            arrow.SetActive(true);
        }
        else
        {
            ui.gameOverUI.SetActive(true);
            ui.brickRemainingText.text = "Bricks Remaining: " + bricksAmount.ToString();
            audioSource.PlayOneShot(loseSound);
            Cursor.visible = true;
        }
    }
}
