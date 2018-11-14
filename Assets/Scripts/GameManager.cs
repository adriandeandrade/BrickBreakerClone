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

        DontDestroyOnLoad(instance);
    }

    #endregion
    
    [SerializeField] private int bricksAmount;
    public int lives;

    [SerializeField] private GameObject paddleWithBallPrefab;
    private GameObject[] bricks;

    private UIManager ui;

    private void Start()
    {
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        bricksAmount = bricks.Length;
        ui = GetComponent<UIManager>();
    }

    private void Update()
    {

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
        else if (lives < 0)
        {
            // TODO: Show game-over screen.
            GameManager.instance.ui.gameOverUI.SetActive(true);
        }
    }
}
