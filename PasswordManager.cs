using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordSecurityExample
{
    public class PasswordManager
    {
        public static (string Hash, string Salt) HashPassword(string password)
        {
            // Gerando um salt aleatório
            byte[] saltBytes = new byte[16];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            // Concatenando senha e salt e gerando o hash
            var hash = ComputeHash(password, salt);

            return (hash, salt);
        }

        public static bool ValidatePassword(string enteredPassword, string storedHash, string storedSalt)
        {
            // Gerar o hash da senha inserida com o salt armazenado
            var hashToValidate = ComputeHash(enteredPassword, storedSalt);

            // Comparar o hash gerado com o hash armazenado
            return hashToValidate == storedHash;
        }

        private static string ComputeHash(string password, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            return Convert.ToBase64String(hash);
        }

        public static bool IsValidPassword(string password)
        {
            // Verificar se a senha atende aos critérios de força
            if (password.Length < 6)
                return false;

            bool hasLetter = false, hasDigit = false, hasSpecialChar = false;
            foreach (char c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else hasSpecialChar = true;
            }

            return hasLetter && hasDigit && hasSpecialChar;
        }
    }
}
