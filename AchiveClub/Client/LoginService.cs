using AchiveClub.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AchiveClub.Client
{
    public class LoginService
    {
        public UserInfo CurrentUser { get; private set; }

        private HttpClient _client;

        public LoginService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> LoginAsync(LoginParams loginParams)
        {
            try
            {
                var result = await _client.PostAsJsonAsync<LoginParams>("/api/Auth/login", loginParams);
                var user = await result.Content.ReadFromJsonAsync<UserInfo>();
                if(user!=null)
                {
                    CurrentUser = user;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
