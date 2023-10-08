using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NetworkPlayer = NetworkServices.NetworkPlayer;


namespace RacingMenu {
    public class MenuController : MonoBehaviour
    {
        [Header("Inputs")] 
        [SerializeField] private TMP_InputField loginInput_login;
        [SerializeField] private TMP_InputField passwordInput_login;
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField loginInput_register;
        [SerializeField] private TMP_InputField passwordInput_register;
        
        [Header("Screens")] 
        [SerializeField] private GameObject MenuLogin;
        [SerializeField] private GameObject MenuRegistration;
        [SerializeField] private GameObject LoadingScreen;

        [Header("Buttons")] 
        [SerializeField] private Button PlayButton;
        [SerializeField] private Button RegisterButton_login;
        [SerializeField] private Button RegisterButton_register;
        [SerializeField] private Button GoBackButton;
        [SerializeField] private Button SoundOnButton;
        [SerializeField] private Button SoundOffButton;
        [SerializeField] private Button ExitButton;

        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI ErrorMessage_register;
        [SerializeField] private TextMeshProUGUI ErrorMessage_login;

        private void Awake()
        {
            LoadingScreen.SetActive(false);
            // audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            // InputController.UIControls.Accept.performed += OnAcceptPressed;
            // InputController.UIControls.NextItem.performed += OnNextItemPressed;
            // InputController.UIControls.Enable();
            // InputController.CarSteering.Disable();
        }

        private void OnDestroy() {
            // InputController.UIControls.Accept.performed -= OnAcceptPressed;
            // InputController.UIControls.NextItem.performed -= OnNextItemPressed;
        }

        private void Start()
        {
            EnableLoginMenu();

            PlayButton.onClick.AddListener(OnPlayButtonClicked);
            RegisterButton_login.onClick.AddListener(OnRegisterButtonClicked);
            RegisterButton_register.onClick.AddListener(OnCreateAccountButtonClicked);
            GoBackButton.onClick.AddListener(OnGoBackButtonClicked);
            SoundOffButton.onClick.AddListener(OnSoundOffButtonClicked);
            SoundOnButton.onClick.AddListener(OnSoundOnButtonClicked);
            ExitButton.onClick.AddListener(OnExitButtonClicked);
            
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
        }

        private void OnExitButtonClicked()
        {
            Application.Quit(); //idk if it works
        }

        private void OnGoBackButtonClicked()
        {
            LoadingScreen.SetActive(true);
            MenuLogin.SetActive(true);
            MenuRegistration.SetActive(false);
            LoadingScreen.SetActive(false);
            
            EnableLoginMenu();
        }

        private void OnRegisterButtonClicked()
        {
            EnableRegistrationMenu();
        }

        private void OnPlayButtonClicked()
        {
            LoadingScreen.SetActive(true);
            NetworkPlayer.LogIn(loginInput_login.text, passwordInput_login.text, () =>
            {
                NetworkServices.Settings.FetchTitleData(() => {
                    SceneManager.LoadSceneAsync("PlayerMenu");
                    ErrorMessage_login.SetText("");
                }, error => {
                    ErrorMessage_login.SetText(error.ErrorMessage);
                    LoadingScreen.SetActive(false);
                });
            }, (error) =>
            {
                Debug.LogError(error.GenerateErrorMessage());
                ErrorMessage_login.SetText(error.ErrorMessage);
                LoadingScreen.SetActive(false);
            });
        }
        
        private void OnCreateAccountButtonClicked()
        {
            LoadingScreen.SetActive(true);
            NetworkPlayer.Register(emailInput.text, loginInput_register.text, passwordInput_register.text, () => {
                NetworkServices.Settings.FetchTitleData(() => {
                    SceneManager.LoadSceneAsync("PlayerMenu");
                    ErrorMessage_register.SetText("");
                }, error => {
                    ErrorMessage_register.SetText(error.ErrorMessage);
                    LoadingScreen.SetActive(false);
                });
            }, (error) =>
            {
                Debug.LogError(error.GenerateErrorMessage());
                ErrorMessage_register.SetText(error.ErrorMessage);
                LoadingScreen.SetActive(false);
            });
        }
        
        private void OnSoundOnButtonClicked()
        {
            DisableSound();
        }

        private void OnSoundOffButtonClicked()
        {
            EnableSound();
        }
        
        private void EnableLoginMenu()
        {
            LoadingScreen.SetActive(true);
            MenuLogin.SetActive(true);
            MenuRegistration.SetActive(false);
            
            loginInput_register.text = "";
            passwordInput_register.text = "";
            emailInput.text = "";
            
            loginInput_login.Select();

            LoadingScreen.SetActive(false);
            
        }
        
        private void EnableRegistrationMenu()
        {
            LoadingScreen.SetActive(true);
            MenuLogin.SetActive(false);
            MenuRegistration.SetActive(true);
            loginInput_login.text = "";
            passwordInput_login.text = "";
            emailInput.Select();
            LoadingScreen.SetActive(false);
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
        //     if (MenuLogin.activeInHierarchy) {
        //         OnPlayButtonClicked();
        //     }else if (MenuRegistration.activeInHierarchy) {
        //         OnCreateAccountButtonClicked();
        //     }
        // }

        // I know I know, this piece of crap is not even worth looking at. But with menu system created by hand and
        // time we have left on this project I call it a masterpiece.
        // private void OnNextItemPressed(InputAction.CallbackContext ctx) {
        //     if (MenuLogin.activeInHierarchy) {
        //         if (loginInput_login.isFocused) {
        //             passwordInput_login.Select();
        //         }else if (passwordInput_login.isFocused) {
        //             loginInput_login.Select();
        //         }
        //     }
        //     else if (MenuRegistration.activeInHierarchy){
        //         if (emailInput.isFocused) {
        //             loginInput_register.Select();
        //         }else if (loginInput_register.isFocused) {
        //             passwordInput_register.Select();
        //         }else if (passwordInput_register.isFocused) {
        //             emailInput.Select();
        //         }
        //     }
        // }
    }
}