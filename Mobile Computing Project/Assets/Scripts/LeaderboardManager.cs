using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public PlayFabManager playFabManager;
    public GameObject userNotLogged;

    // Start is called before the first frame update
    //If user is logged PlayFabManager will show the leaderboard, if not, user gets a message
    void Start()
    {
        playFabManager = this.GetComponent<PlayFabManager>();
        bool loggedIn = playFabManager.IsUserLogged();
        if (loggedIn)
        {
            playFabManager.GetLeaderboardCompletionTime();
        }
        else
        {
            userNotLogged.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
