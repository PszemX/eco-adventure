using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public enum GameState { GS_PAUSEMENU, GS_GAME, GG_LEVELCOMPLETED, GS_GAME_OVER };
public class GameManager : MonoBehaviour
{
    #region Public Variables
    public GameState currentGameState = GameState.GS_GAME;
    public static GameManager instance;
    public Canvas pauseMenuCanvas;
    public Canvas inGameCanvas;
    public TMP_Text scoreText;
    public TMP_Text enemyText;
    public TMP_Text timerText;
    public Image[] cocktailsTab;
    public Image[] keysTab;
    public Image[] livesTab;
    public Image keyImg;
    public int cocktails = 0;
    public int keysFound = 0;
  // 4 iterator po tablicy
    // to trzeba usuwac po kazdym poziomie
    public bool isQuestDone = false;
    public bool isBossKilled = false;
    #endregion

    #region Global Variables
    public static float timer = 0;
    public static int enemiesDie = 0;
    public static int score = 0;
    public static int lives = 3;
    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       timer += Time.deltaTime;
       timerText.text = string.Format("{0:00}:{1:00}", ((timer - 30) / 60), (timer % 60));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GS_GAME)
            {
                PauseMenu();
            }
            else if (currentGameState == GameState.GS_PAUSEMENU) InGame();
        }
    }

    public float getTimer()
    {
        return timer;
    }
    public int getEnemies()
    {
        return enemiesDie;
    }
    public int getScore()
    {
        return score;
    }
    private void Awake()
    {
        InGame();
        instance = this;
        timerText.text = string.Format("{0:00}:{1:00}", timer / 60, timer % 60);
        scoreText.text = score.ToString();
        enemyText.text = enemiesDie.ToString();
        for (int i = 0; i < cocktailsTab.Length; i++)
        {
            cocktailsTab[i].color = Color.gray;
        }
        for (int i = 0; i < keysTab.Length; i++)
        {
            keysTab[i].color = Color.gray;
        }
    }

    public void addCocktail()
    {
        cocktailsTab[cocktails].color = Color.white;
        cocktails++;
    }

    public void useCocktail()
    {
        cocktails--;
        cocktailsTab[cocktails].color = Color.gray;
        AddLives();
    }

    public void KillEnemy()
    {
        enemiesDie++;
        score += 20;
        scoreText.text = score.ToString();
        enemyText.text = enemiesDie.ToString();
    }
    public void DecLives()
    {
        livesTab[lives].enabled = false;
        lives--;
    }
    public void AddLives()
    {
        lives++;
        livesTab[lives].enabled = true;
    }

    public void AddKeys()
    {
        if(keysFound == 0)
        {
            keysTab[keysFound].color = Color.white;
        }

        keysFound++;
    }

    public void useKey()
    {
        keyImg.color = Color.grey;
    }

    public void AddPoints()
    {
        score += 10;
        scoreText.text = score.ToString();
    }

    public void NextLevel()
    {
        isQuestDone = false;
        isBossKilled = false;
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level1")
        {
            SceneManager.LoadSceneAsync("Level2");
        }
        else if(currentScene.name == "Level2")
        {
            SceneManager.LoadSceneAsync("Level3");
        }
        else if(currentScene.name == "Level3")
        {
            SceneManager.LoadSceneAsync("EndAnimation");
        }
    }

    public void EndGame()
    {
        SceneManager.LoadSceneAsync("EndGame");
    }
    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        if (newGameState == GameState.GS_GAME) inGameCanvas.enabled = true;
        else if (newGameState == GameState.GG_LEVELCOMPLETED)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Level1")
            {
                int highScore = PlayerPrefs.GetInt("keyHighScore");
                if (highScore < score) highScore = score;
                PlayerPrefs.SetInt("keyHighScore", highScore);
            }
        }
        else inGameCanvas.enabled = false;
        pauseMenuCanvas.enabled = currentGameState == GameState.GS_PAUSEMENU;
        // LevelCompletedCanvas.enabled = currentGameState == GameState.GG_LEVELCOMPLETED;
    }

    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
        Time.timeScale = 0f;
    }

    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
        Time.timeScale = 1f;
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.GG_LEVELCOMPLETED);
    }

    public void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnReturnToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
