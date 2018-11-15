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
    private GameObject[] bricks;

    public Color oneHitColor;
    public Color twoHitColor;
    public Color invincibleColor;

    private UIManager ui;
    private Ball ball;

    [HideInInspector] public bool gameStarted;

    private void Start()
    {
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        bricksAmount = bricks.Length;
        ui = GetComponent<UIManager>();
        gameStarted = false;

        if (ball == null)
        {
            ball = FindObjectOfType<Ball>();
        }
    }

    private void Update()
    {
        ui.bricksRemainingWorldText.text = "BRICKS: " + bricksAmount.ToString();

        if (bricksAmount <= 0)
        {
            ui.gameOverUI.SetActive(true);
            ui.gameOverText.text = "YOU WIN!";
            gameStarted = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (ball != null)
            {
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
            Cursor.visible = true;
        }
    }
}
