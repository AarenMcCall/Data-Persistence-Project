using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text highscoreName;
    public Text highscore;

    public Text ScoreText;
    public Text playerNameText;

    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        //playerNameText.text = HighscoreManager.Instance.currentPlayer;

        hashighScore();

        //HighscoreName.text = HighscoreManager.Instance.playerName;

        /*
        //loads highscore and player name, if there is one
        HighScorePlayerNameText.text = HighscoreManager.Instance.playerName;
        HighscoreText.text = "Highscore: " + HighscoreManager.Instance.playerScore; */

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        HighscoreManager.Instance.currentScore = m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        checkHighscore();
    }

    public void returnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void checkHighscore()
    {
        if(m_Points > HighscoreManager.Instance.highscore)
        {
            highscoreName.text = HighscoreManager.Instance.currentPlayer;
            highscore.text = "Highscore: " + m_Points;

            HighscoreManager.Instance.highscoreName = HighscoreManager.Instance.currentPlayer;
            HighscoreManager.Instance.highscore = HighscoreManager.Instance.currentScore;

            HighscoreManager.Instance.SaveScore();
        } else
        {
            Debug.Log("No Highscore");
        }
    }

    public void hashighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            playerNameText.text = HighscoreManager.Instance.currentPlayer;
            highscoreName.text = HighscoreManager.Instance.highscoreName;
            highscore.text = "Highscore: " + HighscoreManager.Instance.highscore;
        } else
        {
            playerNameText.text = HighscoreManager.Instance.currentPlayer;
            highscoreName.text = HighscoreManager.Instance.currentPlayer;
        }
    }
}
