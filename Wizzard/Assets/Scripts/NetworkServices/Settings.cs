using System;
using PlayFab;
using PlayFab.ClientModels;

namespace NetworkServices {
    public static class Settings {
        private const string CurrentLeaderboardNameKey = "current_leaderboard";
        private const string CurrentLeaderboardTitleKey = "current_leaderboard_title";
        public static string CurrentLeaderboardName { get; private set; }
        public static string CurrentLeaderboardTitle { get; private set; }

        public static void FetchTitleData(Action successCallback = null, Action<NetworkServiceError> errorCallback = null) {
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest() {
                Keys = null
            }, result => {
                CurrentLeaderboardName = result.Data[CurrentLeaderboardNameKey];
                CurrentLeaderboardTitle = result.Data[CurrentLeaderboardTitleKey];
                successCallback?.Invoke();
            }, error => {
                errorCallback?.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToFetchTitleData,
                    ErrorMessage = "Could not fetch title data."
                });
            });
        }
    }
}