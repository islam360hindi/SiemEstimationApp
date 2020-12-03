using System;
using EstimationApp.Enums;

namespace EstimationApp.Model
{
    public class User
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType UserType { get; set; }
    }
}
