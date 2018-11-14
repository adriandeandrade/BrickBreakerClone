using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text livesText;
    public Text brickRemainingText;
    public Text bricksRemainingWorldText;
    public Text gameOverText;

    private GameManager gameManager;
    public GameObject gameOverUI;

    private void Start()
    {
        gameManager = GameManager.instance;
        livesText.text = "LIVES: " + gameManager.lives.ToString();
        brickRemainingText.text = "Bricks Remaining: " + gameManager.bricksAmount.ToString();
        bricksRemainingWorldText.text = "BRICKS: " + gameManager.bricksAmount.ToString();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
