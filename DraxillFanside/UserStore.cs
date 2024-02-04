using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DraxillFanside
{
    // Manages storage and retrieval of user information
    public static class UserStore
    {
        private static List<User> _users; // Holds all users
        private static readonly string _filePath = "users.json"; // Path to users data file

        // Initializes the UserStore by loading users from the file
        static UserStore()
        {
            LoadUsers();
        }

        // Loads users from a JSON file or initializes an empty list if the file doesnt exist
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

        // Saves the current list of users back to the JSON file
        public static void SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        // Adds a new user defaulting their role to "user" if not specified
        public static void AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = "user"; // Default role
            }
            _users.Add(user);
            SaveUsers(); // Persist changes
        }

        // Retrieves a user by their username if they exist.
        public static User GetUser(string username)
        {
            return _users.FirstOrDefault(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
        }

        // Removes a user from the list by username
        public static void DeleteUser(string username)
        {
            var user = GetUser(username); // Reuse GetUser to find the user
            if (user != null)
            {
                _users.Remove(user);
                SaveUsers(); // Persist changes
            }
        }

        // Checks if a user exists by their username
        public static bool UserExists(string username)
        {
            return _users.Any(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
        }

        // Retrieves the role of a user by their username
        public static string GetUserRole(string username)
        {
            var user = GetUser(username); // Reuse GetUser to simplify logic
            return user?.Role;
        }

        // Returns a list of all users
        public static List<User> GetAllUsers()
        {
            return _users;
        }

        // Updates the role of an existing user
        public static void UpdateUserRole(string username, string newRole)
        {
            var user = GetUser(username); // Reuse GetUser to find the user
            if (user != null)
            {
                user.Role = newRole;
                SaveUsers(); // Persist changes
            }
        }
    }
}
