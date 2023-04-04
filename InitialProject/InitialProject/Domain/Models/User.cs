using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Models
{
    public enum UserRole
    {
        OWNER = 1,
        GUEST1,
        GUEST2,
        GUIDE
    }

    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public User() { }

        public User(string username, string password, UserRole role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Role = ParseUserRole(values[3]);
        }

        private static UserRole ParseUserRole(string value)
        {
            switch (value)
            {
                case "OWNER":
                    return UserRole.OWNER;
                case "GUEST1":
                    return UserRole.GUEST1;
                case "GUEST2":
                    return UserRole.GUEST2;
                case "GUIDE":
                    return UserRole.GUIDE;
            }
            return 0;
        }
    }
}
