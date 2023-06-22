using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text Nametxt;
    public Text HighScoretxt;
   public int highScore;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public Button mainMenubtn;
    //MainMenu m_MainMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            getscore();
           
        }
       mainMenubtn.gameObject.SetActive(false);



        // m_MainMenu = gameObject.AddComponent<MainMenu>();

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


       if(PlayerPrefs.HasKey("playername"))
        {
            string name = PlayerPrefs.GetString("playername");
            Nametxt.text =  name;
        }

    }

    private void Update()
    {
        if (!m_Started)
        {
            getscore();
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


        if(m_GameOver==true)
        {
            savescore();
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points > highScore)
        {
            highScore = m_Points;
         
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        mainMenubtn.gameObject.SetActive(true);
    }

    public void savescore()
    {
        PlayerPrefs.SetInt("Score", highScore);
    }
    public void getscore()
    {
           highScore =  PlayerPrefs.GetInt("Score");

        HighScoretxt.text = "HighScore:" + highScore;

    }
    public void ResetHighScore()
    {
        highScore = 0;
        HighScoretxt.text = "HighScore: " + highScore;
        savescore();
    }

    public void OnResetButtonClick()
    {
        ResetHighScore();
    }


    public void MainMenubtcClicked()
    {
        SceneManager.LoadScene(0);
    }
}
