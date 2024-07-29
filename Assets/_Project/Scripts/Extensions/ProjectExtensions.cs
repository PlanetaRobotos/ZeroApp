using System.Text.RegularExpressions;

namespace _Project.Scripts.Extensions
{
    public static class ProjectExtensions
    {
        private const int MinPasswordLength = 6;

        public static bool IsValidEmail(this string email, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrEmpty(email))
            {
                error = "Email cannot be empty.";
                return false;
            }

            try
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, emailPattern);
            }
            catch
            {
                error = "Invalid email format.";
                return false;
            }
        }
        
        public static bool IsValidPassword(this string password, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrEmpty(password))
            {
                error = "Password cannot be empty.";
                return false;
            }

            error = $"Password must be at least {MinPasswordLength} characters long.";
            return password.Length >= MinPasswordLength;
        }
        
        public static bool IsValidUsername(this string username, out string error)
        {
            bool isValidUsername = !string.IsNullOrEmpty(username);
            error = isValidUsername ? string.Empty : "Username cannot be empty.";
            return isValidUsername;
        }
    }
}