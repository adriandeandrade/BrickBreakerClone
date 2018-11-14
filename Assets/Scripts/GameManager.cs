using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject[] bricks;

    public Color oneHitColor;
    public Color twoHitColor;
    public Color invincibleColor;

    private UIManager ui;

    [HideInInspector] public bool gameStarted;

    private void Start()
    {
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        bricksAmount = bricks.Length;
        ui = GetComponent<UIManager>();
        gameStarted = false;
    }

    private void Update()
    {
        ui.bricksRemainingWorldText.text = "BRICKS: " + bricksAmount.ToString();

        if (bricksAmount <= 0)
        {
            ui.gameOverUI.SetActive(true);
            ui.gameOverText.text = "YOU WIN!";
        }
    }


    public void ResetPlayer()
    {
        if (lives > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Paddle"));
            lives -= 1;
            ui.livesText.text = "LIVES: " + lives.ToString();
            Instantiate(paddleWithBallPrefab, new Vector3(0f, 2f, 0f), Quaternion.identity);
        }
        else
        {
            ui.gameOverUI.SetActive(true);
            ui.brickRemainingText.text = "Bricks Remaining: " + bricksAmount.ToString();
            Cursor.visible = true;
        }
    }
}
