using System;
using System.Linq;
using System.Security.Cryptography;
using QuotingTool.Data;

namespace QuotingTool.Services
{
    public class AuthService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;

        private readonly DatabaseContext _db;

        public AuthService(DatabaseContext db)
        {
            _db = db;
        }

        public User? Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = _db.GetUserByUsername(username.Trim());
            if (user == null || !user.IsActive)
                return null;

            return VerifyPassword(password, user.PasswordHash, user.PasswordSalt) ? user : null;
        }

        public string? EnsureDefaultAdministrator()
        {
            if (_db.GetUserByUsername("admin") != null)
                return null;

            var initialPassword = Environment.GetEnvironmentVariable("QUOTINGTOOL_DEFAULT_ADMIN_PASSWORD");
            if (string.IsNullOrWhiteSpace(initialPassword))
                initialPassword = GenerateStrongPassword();
            CreatePasswordHash(initialPassword, out var hash, out var salt);
            _db.InsertUser(new User
            {
                Username = "admin",
                DisplayName = "Administrator",
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = UserRole.Administrator,
                Email = "admin@local",
                IsActive = true
            });

            return initialPassword;
        }

        public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(SaltSize);
            hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
        }

        public static bool VerifyPassword(string password, byte[] expectedHash, byte[] salt)
        {
            if (expectedHash.Length == 0 || salt.Length == 0)
                return false;

            var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
            return CryptographicOperations.FixedTimeEquals(actual, expectedHash);
        }

        public static bool IsStrongPassword(string password)
            => password.Length >= 8
               && password.Any(char.IsUpper)
               && password.Any(char.IsLower)
               && password.Any(char.IsDigit)
               && password.Any(ch => !char.IsLetterOrDigit(ch));

        public static string GenerateStrongPassword(int length = 14)
        {
            const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%^&*";
            var chars = new char[length];
            var bytes = RandomNumberGenerator.GetBytes(length);
            for (int i = 0; i < length; i++)
                chars[i] = alphabet[bytes[i] % alphabet.Length];

            if (!chars.Any(char.IsUpper)) chars[0] = 'A';
            if (!chars.Any(char.IsLower)) chars[1] = 'a';
            if (!chars.Any(char.IsDigit)) chars[2] = '2';
            if (!chars.Any(ch => !char.IsLetterOrDigit(ch))) chars[3] = '!';
            return new string(chars);
        }
    }
}
