using System;
namespace EstimationApp.Model
{
    public class LoginResult
    {
        public bool IsAuthenticated { get; set; }

        public User User { get; set; }
    }
}
