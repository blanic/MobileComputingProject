using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    //LEVEL INDICES
    private static int NUMBER_OF_SCENES = 9;
    private static int MENU_INDEX = 0;
    private static int LOGGED_IN_MENU = 1;    
    private static int LOGIN_INDEX = 2;
    private static int REGISTRATION_INDEX = 3;
    private static int LEADERBOARD_INDEX = 4;
    private static int SETTINGS_INDEX = 5;
    private static int GAME_OVER_INDEX = 6;
    private static int END_GAME_INDEX = 7;
    private static int FIRST_PLAYABLE_INDEX_LEVEL = 8;

    private static int LAST_LOADED_LEVEL = 0;

   
    private static float startTime;

    //LEVEL TRANSITION ANIMATION
    public Animator transition;
    public float transitionTime = 1f;

    //CURRENT LEVEL AUDIOCLIP
    public AudioClip audioClip;

   private void Start()
    {
        //Calls PlayMusic every time a scene is loaded, to check if audioClip must be changed
        MusicManager musicManager = FindAnyObjectByType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.PlayMusic(audioClip);
        }
    }




    /*********************************** LOAD LEVEL **********************************************************************/

    // Load anothere level triggering a UI Disappearing Animation
    IEnumerator LoadSceneWithTransition(int levelIndex)
    {
        transition.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);

        LAST_LOADED_LEVEL = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }




    /*********************************** GAME RELATED METHODS *************************************************************/

    //Load next level, or EndGame scene if there is none
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;

        if (nextSceneIndex > (NUMBER_OF_SCENES + 1))
        {
            SaveGameCompletionTime();
            StartCoroutine(LoadSceneWithTransition(END_GAME_INDEX));
        }
        else
        {
            StartCoroutine(LoadSceneWithTransition(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    //Start New Game
    public void StartGame()
    {
        InitializeTimer();
        StartCoroutine(LoadSceneWithTransition(FIRST_PLAYABLE_INDEX_LEVEL));
    }

    //Restart the game level
    public void RestartLevel()
    {
        StartCoroutine(LoadSceneWithTransition(LAST_LOADED_LEVEL));
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


    /*********************************** UI RELATED METHODS ***************************************************************/

    //LOAD UI SCENES
    public void LoadLoginPage()
    {
        StartCoroutine(LoadSceneWithTransition(LOGIN_INDEX));
    }

    public void LoadRegistrationPage()
    {
        StartCoroutine(LoadSceneWithTransition(REGISTRATION_INDEX));
    }

    public void LoadMenu()
    {
        PlayFabManager playFabManager = this.GetComponent<PlayFabManager>();
        bool loggedIn = playFabManager.IsUserLogged();

        if (loggedIn)
        {
            StartCoroutine(LoadSceneWithTransition(LOGGED_IN_MENU));
        }
        else
        {
            StartCoroutine(LoadSceneWithTransition(MENU_INDEX));
        }
    }

    public void LoadLeaderboardPage()
    {
        StartCoroutine(LoadSceneWithTransition(LEADERBOARD_INDEX));
    }

    public void LoadSettingsPage()
    {
        StartCoroutine(LoadSceneWithTransition(SETTINGS_INDEX));
    }

    public void LoadGameOverPage()
    {
        StartCoroutine(LoadSceneWithTransition(GAME_OVER_INDEX));
    }

    public void LoadEndGamePage()
    {
        StartCoroutine(LoadSceneWithTransition(END_GAME_INDEX));
    }

    //Quit Application
    public void Quit()
    {
        Application.Quit();
    }




    /*********************************** LEADERBOARD ***************************************************************/

    private void InitializeTimer()
    {
        startTime = Time.time;
    }

    //Called when game is completed to update Leaderboard
    private void SaveGameCompletionTime()
    {
        float endTime = Time.time;
        float totalTime = endTime - startTime;

        PlayFabManager playFabManager = this.GetComponent<PlayFabManager>();
        playFabManager.SendLeaderboardCompletionTime(Mathf.FloorToInt(totalTime));
    }

    
    

    
    

    

}
