using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text livesText;

    private GameManager gameManager;
    public GameObject gameOverUI;

    private void Start()
    {
        gameManager = GameManager.instance;
        livesText.text = "LIVES: " + gameManager.lives.ToString();
    }


}
