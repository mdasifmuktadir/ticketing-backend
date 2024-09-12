using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;
using System.Threading.Tasks;
using Eapproval.Helpers;
using Eapproval.Models;
using Eapproval.Helpers.IHelpers;

namespace Eapproval.Helpers
{
    public class UserApi:IUserApi
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://192.168.0.11:8420/api/Emloyees";
        private const int CompanyId = 1;
        private const string ApiKey = "27d34740-c3d4-4938-9260-b5ba3a62922c";

        public UserApi(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<string?> GetTeams()
        {
            try
            {
                var parameters = new Dictionary<string, string>
            {
                {"companyid", CompanyId.ToString() }
            };

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

                var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(BaseUrl, parameters));

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                else
                {
                    var errorMessage = $"Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}";
                    return errorMessage;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            };


        }
    }
}
