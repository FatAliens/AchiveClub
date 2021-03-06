using AchiveClub.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AchiveClub.Client
{
    public class LoginService
    {
        public UserInfo CurrentUser { get; private set; }
        public bool IsAuthorized { get; private set; } = false;

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
                    IsAuthorized = true;

                    return true;
                }
                else
                {
                    Logout();

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            IsAuthorized = false;
        }
    }
}
