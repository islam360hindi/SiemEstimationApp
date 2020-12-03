using System;
using System.Threading.Tasks;
using EstimationApp.Model;

namespace EstimationApp.DataAccess
{
    public interface ILoginDataAccess
    {
        Task<LoginResult> Login(string username, string password);

        Task Logout();
    }
}
