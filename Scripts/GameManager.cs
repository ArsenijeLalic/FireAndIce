using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameObject player;
    public bool gameIsOn = true;
    GameObject gameOverMenu;
    private bool initialized = false;
    GameObject pauseMenu;
    private bool won = false;
    GameObject castleCleansed;

    private void Awake()
    {
        if (Instance != null)
        {
            Instance.initialized = false;
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            Instance.init();
        }
    }

    private void init()
    {
        gameIsOn = true;
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        gameOverMenu = GameObject.Find("GameOverMenu");
        gameOverMenu.SetActive(false);
        castleCleansed = GameObject.Find("VictoryScreen");
        if (castleCleansed != null)
            castleCleansed.SetActive(false);
        player = GameObject.Find("Player");
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (won)
            return;
        if (!initialized)
            Instance.init();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void GameOver()
    {
        gameIsOn = false;
        gameOverMenu.SetActive(true);
    }

    public void WelcomeScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Victory()
    {
        won = true;
        castleCleansed.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        if (player.GetComponent<HealthSystem>().dead)
        {
            gameOverMenu.SetActive(!gameOverMenu.activeInHierarchy);
        }
        else
        {
            gameIsOn = !gameIsOn;
        }
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }
}
