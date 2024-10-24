using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonHandler : MonoBehaviour
{
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void Pause()
    {
        gm.PauseGame();
    }

    public void Restart()
    {
        gm.Restart();
    }

    public void MainMenu()
    {
        gm.WelcomeScreen();
    }
}
