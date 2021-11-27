using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AchiveClub.Shared.DTO;

namespace AchiveClub.Client
{
    public class RegistrationService
    {
        private HttpClient _client;

        public RegistrationService(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public async Task<bool> Registrate(RegisterParams registerParams)
        {
            var result = await _client.PostAsJsonAsync<RegisterParams>("/api/Auth/register", registerParams);
            return result.IsSuccessStatusCode;
        }
    }
}
