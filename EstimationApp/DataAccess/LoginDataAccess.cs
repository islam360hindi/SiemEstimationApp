using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EstimationApp.Model;
using Newtonsoft.Json;

namespace EstimationApp.DataAccess
{
    public class LoginDataAccess : ILoginDataAccess
    {
        public async Task<LoginResult> Login(string username, string password)
        {
            await Task.Delay(1000); //Mocking Api delay
            var assembly = Assembly.GetExecutingAssembly();
            var loginTableJsonResourceName = "EstimationApp.Assets.LoginTable.json";
            using (var stream = assembly.GetManifestResourceStream(loginTableJsonResourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var jsonText = await reader.ReadToEndAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(jsonText);
                    if (users == null || !users.Any(x => x.Username.ToLower().Equals(username.ToLower())))
                        return new LoginResult();
                    var user = users.FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()));
                    return new LoginResult
                    {
                        IsAuthenticated = true,
                        User = user
                    };
                }
            }
        }

        public async Task Logout()
        {
            await Task.Delay(1000); //mocking logout
        }
    }
}
