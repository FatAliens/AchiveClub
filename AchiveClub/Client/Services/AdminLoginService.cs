using AchiveClub.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AchiveClub.Client
{
    public class AdminLoginService
    {
        public AdminInfo CurrentAdmin { get; private set; }
        public bool IsAuthorized { get; private set; } = false;

        private HttpClient _client;

        public AdminLoginService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> LoginAsync(LoginParams loginParams)
        {
            try
            {
                var result = await _client.PostAsJsonAsync<LoginParams>("/api/Auth/admin", loginParams);
                var admin = await result.Content.ReadFromJsonAsync<AdminInfo>();
                if(admin!=null)
                {
                    CurrentAdmin = admin;
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
            CurrentAdmin = null;
            IsAuthorized = false;
        }
    }
}
