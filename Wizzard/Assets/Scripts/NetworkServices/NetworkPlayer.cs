using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace NetworkServices {
    public static class NetworkPlayer {
        public static string Login { get; private set; }
        public static string DisplayName { get; private set; }
        public static bool NewlyRegistered { get; private set; }
        
        public static void LogIn(string login, string password, Action successCallback, Action<NetworkServiceError> errorCallback) {
            
            if (!Utils.LoginCheck(login, out string errorLoginMessage)) {
                errorCallback.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToLogIn,
                    ErrorMessage = errorLoginMessage
                });
                return;
            }

            if (!Utils.PasswordCheck(password, out string errorPasswordMessage)) {
                errorCallback.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToLogIn,
                    ErrorMessage = errorPasswordMessage
                });
                return;
            }

            GetPlayerCombinedInfoRequestParams profileInfoParams = new GetPlayerCombinedInfoRequestParams() {
                GetPlayerProfile = true
            };
            
            PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest() {
                    Username = login,
                    Password = password,
                    InfoRequestParameters = profileInfoParams
            },
                (result) => {
                    var displayName = String.Empty;
                    try {
                        displayName = result.InfoResultPayload.PlayerProfile.DisplayName;
                    }
                    catch (Exception e) {
                        Debug.Log("User does not have displayName set (probably logged in to account from other Coin game). Updating...");
                        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest() {
                            DisplayName = login
                        },
                            (nameResult => {
                                displayName = nameResult.DisplayName;
                            }),
                            (error => {
                                Debug.Log($"Could not update display name: {error.GenerateErrorReport()}");
                            }));
                    }
                    DisplayName = string.IsNullOrEmpty(displayName) ? login : displayName;
                    Login = login;
                    successCallback.Invoke();
                },
                (error) => {
                    errorCallback.Invoke(new NetworkServiceError() {
                        ErrorCode =  NetworkServiceErrorCode.FailedToLogIn,
                        ErrorMessage = error.ErrorMessage
                    });
                });
        }

        public static void Register(string email, string login, string password,
            Action successCallback,
            Action<NetworkServiceError> errorCallback) {
            
            if (!Utils.EmailCheck(email, out string errorEmailMessage)) {
                errorCallback.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToRegister,
                    ErrorMessage = errorEmailMessage
                });
                return;
            }

            if (!Utils.LoginCheck(login, out string errorLoginMessage)) {
                errorCallback.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToRegister,
                    ErrorMessage = errorLoginMessage
                });
                return;
            }

            if (!Utils.PasswordCheck(password, out string errorPasswordMessage)) {
                errorCallback.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToRegister,
                    ErrorMessage = errorPasswordMessage
                });
                return;
            }

            PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest() {
                Email = email,
                Username =  login,
                Password = password,
                DisplayName = login,
                RequireBothUsernameAndEmail = false
            },
                (result) =>
                {
                    Login = login;
                    DisplayName = login;
                    NewlyRegistered = true;
                    successCallback.Invoke();
                },
                (error) => {
                    errorCallback.Invoke(new NetworkServiceError() {
                        ErrorCode = NetworkServiceErrorCode.FailedToRegister,
                        ErrorMessage = error.ErrorMessage
                    });
                });
        }

        public static void LogOut() {
            PlayFabClientAPI.ForgetAllCredentials();
            ForgetCredentials();
        }

        private static void ForgetCredentials() {
            Login = string.Empty;
            DisplayName = string.Empty;
        }
    }
}