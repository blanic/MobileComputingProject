using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class PlayFabManager : MonoBehaviour
{
    //UI ELEMENTS - NEEDED ONLY IN LOGIN AND REGISTER SCENES
    [Header("UI")]
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;
    public InputField username;

    //UI ELEMENTS - NEEDED ONLY IN LEADERBOARD PAGE
    public GameObject leaderboardRowPrefab;
    public Transform leaderboardTableTransform;

    // Start is called before the first frame update
    void Start()
    {
       // Login(); 
    }




    /**************************************************************** REGISTRATION *********************************************************/

    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "The password is too short";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            DisplayName = username.text,
            RequireBothUsernameAndEmail = false,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in";
        StartCoroutine(WaitFor(5));
        GetComponent<GameManager>().LoadMenu();
    }




    /**************************************************************** LOGIN ****************************************************************/


    //LOGIN

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    
    public void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        StartCoroutine(WaitFor(5));
        GetComponent<GameManager>().LoadMenu();
        Debug.Log("Successful login/account create!");
    }


    //LOGIN AS GUEST
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }


    //IS USER LOGGED

    public bool IsUserLogged()
    {

        return PlayFabClientAPI.IsClientLoggedIn();


    }




    /**************************************************************** RESET PASSWORD *********************************************************/

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "92F15"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset mail sent!";
    }




    /**************************************************************** LEADERBOARD *********************************************************/

    //UPDATE LEADERBOARD
    public void SendLeaderboardCompletionTime(int score)
    {
        Debug.Log(score);

        if (!IsUserLogged())
        {
            return;
        }

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "CompletionTime",
                Value = score + 1000
            }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sent Successfully");
    }


    //GET LEADERBOARD
    public void GetLeaderboardCompletionTime()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "CompletionTime",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {

        String completionTime;
        String username;
        int rank = result.Leaderboard.Count;

        foreach (var item in result.Leaderboard)
        {
            GameObject newRow = Instantiate(leaderboardRowPrefab, leaderboardTableTransform);
            Text[] texts = newRow.GetComponentsInChildren<Text>();

            //RANK
            //texts[0].text = (item.Position + 1).ToString();  
            texts[0].text = rank.ToString();   // Rank is reversed

            rank--;

            //USERNAME OR ID
            if (item.DisplayName != null)
            {
                username = item.DisplayName.ToString();
            }
            else
            {
                username = item.PlayFabId;
            }
            texts[1].text = username;

            //SCORE
            int totalTimeInSeconds = item.StatValue;

            int hours = totalTimeInSeconds / 3600;
            int minutes = (totalTimeInSeconds % 3600) / 60;
            int seconds = totalTimeInSeconds % 60;

            // Format the time in "hours:minutes:seconds" format
            completionTime = string.Format("{0}:{1:00}:{2:00}", hours, minutes, seconds);

            texts[2].text = completionTime;


            Debug.Log(string.Format("RANK: {0} | USERNAME: {1} | SCORE: {2}",
                item.Position, item.DisplayName, item.StatValue));
        }


    }




    /**************************************************************** ERROR *************************************************************/

    void OnError(PlayFabError error)
    {
        if(messageText != null)
        {
            messageText.text = error.ErrorMessage;
        }
        Debug.Log(error.GenerateErrorReport());
    }


    /**************************************************************** WAIT *************************************************************/




    IEnumerator WaitFor(float seconds)
    {
        // Wait for the specified time (use WaitForSecondsRealtime for real-world time)
        yield return new WaitForSecondsRealtime(seconds);

        // Code here will execute after the wait
        Debug.Log("Wait is over.");
    }

}