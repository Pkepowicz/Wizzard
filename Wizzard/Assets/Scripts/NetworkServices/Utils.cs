using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PlayFab.ClientModels;

namespace NetworkServices {
    
    public static class Utils
    {
        public static int TimeToStat(float score) {
            // return 9_999_999 - (int)(Math.Round(score, 3) * 1000);
            return (int)score;
        }
        

        public static float StatToTime(int score) {
            // return (9_999_999 - score) / 1000f;
            return score;
        }

        public static string IndexToGateName(int index) {
            return "gate_" + index.ToString();
        }

        public static bool LoginCheck(string login, out string errorMessage) {
            if (string.IsNullOrEmpty(login)) {
                errorMessage = "You must specify login.";
                return false;
            }

            if (login.Length < 3 | login.Length > 20) {
                errorMessage = "Login must be 3-20 characters long.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public static bool PasswordCheck(string password, out string errorMessage) {
            if (string.IsNullOrEmpty(password)) {
                errorMessage = "You must specify password.";
                return false;
            }
            if (password.Length < 6 | password.Length > 100) {
                errorMessage = "Password must be 6-100 characters long.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
        
        public static bool EmailCheck(string email, out string errorMessage) {
            if (string.IsNullOrEmpty(email)) {
                errorMessage = "You must specify email.";
                return false;
            }
            
            errorMessage = string.Empty;
            return true;
        }
    }
}