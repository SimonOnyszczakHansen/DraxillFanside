using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DraxillFanside.Pages;

namespace DraxillFanside
{
    public static class UserStore
    {
        private static List<User> _users;
        private static readonly string _filePath = "users.json";

        static UserStore()
        {
            LoadUsers();
        }
        private static void LoadUsers()
        {
            if (File.Exists(_filePath)) 
            {
                string json = File.ReadAllText(_filePath);
                _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
            }
        }
        public static void SaveUsers() 
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(_filePath, json );
        }
        public static void AddUser(User user )
        {
            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = "user";
            }
            _users.Add(user);
            SaveUsers();
        }
        public static User GetUser(string username)
        {
            return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        public static void DeleteUser(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                _users.Remove(user);
                SaveUsers();
            }
        }
        public static bool UserExists(string username)
        {
            return _users.Any(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
        }
        public static string GetUserRole(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
            return user?.Role;
        }
        public static List<User> GetAllUsers()
        {
            return _users;
        }
        public static void UpdateUserRole(string username, string newRole)
        {
            var user = _users.FirstOrDefault(u =>u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                user.Role = newRole;
                SaveUsers();
            }
        }
    }
}
