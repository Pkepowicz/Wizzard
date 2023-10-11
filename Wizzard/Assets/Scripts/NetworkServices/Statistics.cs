using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

namespace NetworkServices {
    public static class Statistics {
        public static float BestScore { get; private set; }

        /// <summary>
        /// Updates gates times for user.
        /// </summary>
        public static void UpdateGates(List<float> gates, Action successCallback = null, Action<NetworkServiceError> errorCallback = null) {
            Dictionary<string, string> gatesDictionary = new Dictionary<string, string>();

            int gateIndex = 0;
            foreach (var gate in gates) {
                gatesDictionary.Add(Utils.IndexToGateName(gateIndex++), Utils.TimeToStat(gate).ToString());
            }

            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                Data = gatesDictionary
            },
                (result) => successCallback?.Invoke(),
                (error) => errorCallback?.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToUpdateGates,
                    ErrorMessage = error.ErrorMessage
                }));
        }
        
        /// <summary>
        /// Returns gates times or empty list if statistics couldn't be found.
        /// </summary>
        /// <param name="numberOfGates">How many gates to look for.</param>
        public static void FetchGates(int numberOfGates, Action<List<float>> successCallback, Action<NetworkServiceError> errorCallback = null) {
            List<float> gates = new List<float>();
            List<string> dataKeys = new List<string>();

            for (int i = 0; i < numberOfGates; i++) {
                dataKeys.Add(Utils.IndexToGateName(i));
            }
            
            PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                Keys = dataKeys
            }, 
                (result) => {
                    foreach (var dataKey in dataKeys) {
                        try {
                            gates.Add(Utils.StatToTime(int.Parse(result.Data[dataKey].Value)));
                        }
                        catch (KeyNotFoundException e) {
                            gates.Add(0f);
                        }
                    }
                    successCallback.Invoke(gates);
                },
                (error) => errorCallback?.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToFetchGates,
                    ErrorMessage = error.ErrorMessage
                }));
        }
        
        /// <summary>
        /// Updates best score of the player if the given score is better than previous, otherwise does nothing.
        /// </summary>
        public static void UpdateBestScore(float score, Action successCallback = null, Action<NetworkServiceError> errorCallback = null) {
            BestScore = (score < BestScore | BestScore == 0f) ? (float)Math.Round(score, 3) : BestScore;
            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest() { 
                    Statistics = new List<StatisticUpdate>() {new StatisticUpdate(){StatisticName = Settings.CurrentLeaderboardName, Value = Utils.TimeToStat(score)}} 
                },
                (result) => successCallback?.Invoke(),
                (error) => errorCallback?.Invoke(new NetworkServiceError() {
                    ErrorCode = NetworkServiceErrorCode.FailedToUpdateStatistics,
                    ErrorMessage = error.ErrorMessage
                }));
        }

        /// <summary>
        /// Returns best score for the player or 0 if score was not found.
        /// </summary>
        public static void FetchBestScore(Action<float> successCallback, Action<NetworkServiceError> errorCallback = null) {
            PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest() {
                    StatisticNames = new List<string>() { Settings.CurrentLeaderboardName }
                },
                (result) => {
                    BestScore = result.Statistics.Count < 1 ? 0f : Utils.StatToTime(result.Statistics[0].Value);
                    successCallback.Invoke(BestScore);
                },
                (error) => errorCallback?.Invoke(new NetworkServiceError() {
                        ErrorCode = NetworkServiceErrorCode.FailedToFetchStatistics,
                        ErrorMessage = error.ErrorMessage
                    }));
        }
    }
}