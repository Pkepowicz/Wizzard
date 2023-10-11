using System;
using PlayFab;
using PlayFab.ClientModels;

namespace NetworkServices {
    public static class Leaderboards {
        public static void FetchBestScoreLeaderboard(int maxCount,
            Action<PlayerLeaderboardEntry[]> successCallback,
            Action<NetworkServiceError> errorCallback) {
            
            PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest() {
                StatisticName = Settings.CurrentLeaderboardName,
                MaxResultsCount = maxCount
            },
                (result) => successCallback.Invoke(result.Leaderboard.ToArray()),
                (error) => errorCallback.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToFetchLeaderboard,
                    ErrorMessage = error.ErrorMessage
                }));
        }

        public static void FetchBestScoreAroundPlayer(int maxCount,
            Action<PlayerLeaderboardEntry[]> successCallback,
            Action<NetworkServiceError> errorCallback) {

            PlayFabClientAPI.GetLeaderboardAroundPlayer(new GetLeaderboardAroundPlayerRequest() {
                    StatisticName = Settings.CurrentLeaderboardName,
                    MaxResultsCount = maxCount
                },
                (result) => successCallback.Invoke(result.Leaderboard.ToArray()),
                (error) => errorCallback.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToFetchLeaderboard,
                    ErrorMessage = error.ErrorMessage
                }));
        }
        
        public static void FetchPlayerPositionInLeaderboard(
            Action<int> successCallback,
            Action<NetworkServiceError> errorCallback = null) {

            PlayFabClientAPI.GetLeaderboardAroundPlayer(new GetLeaderboardAroundPlayerRequest() {
                    StatisticName = Settings.CurrentLeaderboardName,
                    MaxResultsCount = 1
                },
                (result) => successCallback.Invoke(result.Leaderboard[0].Position + 1),
                (error) => errorCallback?.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToFetchLeaderboard,
                    ErrorMessage = error.ErrorMessage
                }));
        }
    }
}