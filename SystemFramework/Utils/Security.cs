namespace Redarbor.SystemFramework.Utils
{
    public static class Security
    {

        public static string EncodePassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string passwordToVerify)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToVerify, password);
        }
    }
}