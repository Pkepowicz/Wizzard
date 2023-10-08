namespace NetworkServices {

    public enum NetworkServiceErrorCode {
        FailedToLogIn,
        FailedToRegister,
        FailedToUpdateStatistics,
        FailedToFetchStatistics,
        FailedToUpdateGates,
        FailedToFetchGates,
        FetchedEmptyValue,
        FailedToFetchLeaderboard,
        FailedToFetchTitleData
    }
    
    public class NetworkServiceError {
        public NetworkServiceErrorCode ErrorCode;
        public string ErrorMessage;

        public string GenerateErrorMessage() {
            return ErrorCode.ToString() + ": " + ErrorMessage;
        }
    }
}