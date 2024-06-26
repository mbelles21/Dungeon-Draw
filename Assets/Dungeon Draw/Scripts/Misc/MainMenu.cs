using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private SceneRouter _sceneRouter;

    public AudioSource src;
    public AudioClip buttonSelectSound;
    public AudioClip quitSound;

    private void Start()
    {
        _sceneRouter = GameManager.Instance.GetSceneRouter();
    }

    // starting a new game
    public void StartGame()
    {
        PlaySFX(buttonSelectSound);
        LevelTracker.levelsVisited = 0;
        LevelTracker.floorsVisited = 1;
        // TODO: overwrite totalLevels and floors when starting new game?
        _sceneRouter.ToMap();
    }

    public void ContinueGame()
    {
        PlaySFX(buttonSelectSound);
        
        int totalLevels = PlayerPrefs.GetInt("TotalLevels", 0);
        LevelTracker.levelsVisited = totalLevels;
        
        int totalFloors = PlayerPrefs.GetInt("TotalFloors", 0);
        LevelTracker.floorsVisited = totalFloors;
        
        _sceneRouter.ToMap();
    }

    public void Credits()
    {
        PlaySFX(buttonSelectSound);
        _sceneRouter.ToCredits();
    }

    public void ReturnToMenu()
    {
        PlaySFX(quitSound);
        _sceneRouter.ToMainMenu();
    }

    public void QuitGame()
    {
        PlaySFX(quitSound);
        
        // TODO: make this actually quit the game
        Application.Quit();
        Debug.Log("quitting game");
    }

    public void PlaySFX(AudioClip sfx)
    {
        src = gameObject.GetComponent<AudioSource>();
        src.clip = sfx;
        src.Play();
    }
}
