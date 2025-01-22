using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TraderApp.Repositories;

namespace TraderApp.Managers
{
    public static class UserManager
    {
        private static string UsersFilePath = @"..\..\..\SaveFolder\RegUsers.JSON";

        public static List<User> LoadUsers()
        {
            if (!File.Exists(UsersFilePath))
            {
                return new List<User>();
            }

            string registeredUsers = File.ReadAllText(UsersFilePath);
            return JsonConvert.DeserializeObject<List<User>>(registeredUsers) ?? new List<User>();
        }

        public static void SaveUsers(List<User> users)
        {
            string registeredUsers = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(UsersFilePath, registeredUsers);
        }

        public static bool AuthenticateUser(string email, string password)
        {
            var users = LoadUsers();
            return users.Exists(user => user.Email == email && user.Password == password);
        }

        public static void RegisterUser(User newUser)
        {
            var users = LoadUsers();
            if (!users.Exists(user => user.Email == newUser.Email))
            {
                users.Add(newUser);
                SaveUsers(users);
            }
            else
            {
                throw new Exception("A user with this email already exists.");
            }
        }

        public static void AddUser(User user)
        {
            var users = LoadUsers();
            users.Add(user);
            SaveUsers(users);
        }

        public static void UpdateUser(User updatedUser)
        {
            var users = LoadUsers();
            var user = users.Find(u => u.ID == updatedUser.ID);
            if (user != null)
            {
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
                SaveUsers(users);
            }
        }

        public static void DeleteUser(int userId)
        {
            var users = LoadUsers();
            var user = users.Find(u => u.ID == userId);
            if (user != null)
            {
                users.Remove(user);
                SaveUsers(users);
            }
        }
    }
}
