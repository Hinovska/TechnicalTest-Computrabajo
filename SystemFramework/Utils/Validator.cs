using System.Text.RegularExpressions;

namespace Redarbor.SystemFramework.Utils
{
    public static class Validator
    {
        private static Regex regex = new Regex(@"^([\w.-]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        public static bool IsValidEmailAddress(string email)
        {
            Match match = regex.Match(email);
            return match.Success;
        }

    }
}