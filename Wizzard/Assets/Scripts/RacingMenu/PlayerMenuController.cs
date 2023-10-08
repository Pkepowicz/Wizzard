using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using NetworkServices;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NetworkPlayer = NetworkServices.NetworkPlayer;

public class PlayerMenuController : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button LogoutButton;
    [SerializeField] private Button SoundOnButton;
    [SerializeField] private Button SoundOffButton;
    [SerializeField] private Button ExitButton;

    
    [Header("References")]
    [SerializeField] private GameObject LeaderboardContainer;
    [SerializeField] private GameObject ListElement;

    [Header("Texts")] 
    [SerializeField] private TextMeshProUGUI DisplayName;
    [SerializeField] private TextMeshProUGUI BestScore;
    [SerializeField] private TextMeshProUGUI Position;
    [SerializeField] private TextMeshProUGUI LeaderboardTitle;

    [Header("Screens")]
    [SerializeField] private GameObject LoadingScreen;

    // private AudioManager audioManager;
    private Color _color = new Color(0.94f, 0.65f, 0.31f);
    private void Awake()
    {
        LoadingScreen.SetActive(false);
        LogoutButton.onClick.AddListener(OnLogoutButtonClicked);
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
        SoundOnButton.onClick.AddListener(OnSoundOnButtonClicked);
        SoundOffButton.onClick.AddListener(OnSoundOffButtonClicked);
        ExitButton.onClick.AddListener(OnExitButtonClicked);
        
        // audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        // InputController.UIControls.Accept.performed += OnAcceptPressed;
        // InputController.UIControls.Back.performed += OnBackPressed;
        // InputController.UIControls.Enable();
    }

    private void OnExitButtonClicked()
    {
        NetworkPlayer.LogOut();
        Application.Quit();
    }

    private void OnDestroy() {
        // InputController.UIControls.Accept.performed -= OnAcceptPressed;
        // InputController.UIControls.Back.performed -= OnBackPressed;
    }

    void Start()
    {
        
        string displayName = NetworkPlayer.DisplayName;
        DisplayName.SetText("Hello " + displayName + "!");
        LeaderboardTitle.SetText("Active leaderboard: " + NetworkServices.Settings.CurrentLeaderboardTitle);
        
        Statistics.FetchBestScore((score) =>
        {
            if (score == 0)
            {
                BestScore.SetText("No score yet");
            }
            else
            { 
                BestScore.SetText(score + "s");
                Leaderboards.FetchPlayerPositionInLeaderboard((pos) =>
                {
                    Position.SetText("Your current position: " + pos);
                }, error =>
                {
                    Debug.LogError(error);
                });
            }
            
        }, error =>
        {
            Debug.LogError(error);
        });


        
        // if (audioManager.IsMuted())
        // {
        //     SoundOffButton.gameObject.SetActive(true);
        //     SoundOnButton.gameObject.SetActive(false);
        // }
        // else
        // {
        //     SoundOffButton.gameObject.SetActive(false);
        //     SoundOnButton.gameObject.SetActive(true);
        // }
        
        Leaderboards.FetchBestScoreLeaderboard(10, entries =>
        {
            CreateLeaderboard(entries);
        }, error =>
        {
            Debug.LogError(error);
        });
        
        Statistics.FetchBestScore((f => Debug.Log($"Fetched best score for player {displayName}: {f}")));
    }

    private void CreateLeaderboard(PlayerLeaderboardEntry[] entries)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            GameObject listElement = Instantiate(ListElement, LeaderboardContainer.transform);
            listElement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((entries[i].Position + 1) + ".");
            listElement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(entries[i].DisplayName + " :");
            listElement.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(Utils.StatToTime(entries[i].StatValue).ToString(CultureInfo.InvariantCulture) + "s");
            
            if (entries[i].DisplayName == NetworkPlayer.DisplayName)
            {
                listElement.transform.GetComponent<Image>().color = _color;
            }
            
        }
    }
    
    
    private void OnLogoutButtonClicked()
    {
        LoadingScreen.SetActive(true);
        NetworkPlayer.LogOut();
        SceneManager.LoadSceneAsync("MainMenu");    
    }

    private void OnPlayButtonClicked()
    {
        LoadingScreen.SetActive(true);
        // InputController.UIControls.Disable();
        SceneManager.LoadSceneAsync("Map");
    }
    
    private void OnSoundOnButtonClicked()
    {
        DisableSound();
    }

    private void OnSoundOffButtonClicked()
    {
        EnableSound();
    }
    
    private void EnableSound()
    {
        SoundOffButton.gameObject.SetActive(false);
        SoundOnButton.gameObject.SetActive(true);
        // audioManager.ToggleMute();
    }
        
    private void DisableSound()
    {
        SoundOffButton.gameObject.SetActive(true);
        SoundOnButton.gameObject.SetActive(false);
        // audioManager.ToggleMute();
    }

    // private void OnAcceptPressed(InputAction.CallbackContext ctx) {
    //     OnPlayButtonClicked();
    // }
    //
    // private void OnBackPressed(InputAction.CallbackContext ctx) {
    //     OnLogoutButtonClicked();
    // }
}
