using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PostingControlService.Api.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DailyConsolidatedService.Api;
using PostingControlService.Api;   

namespace DailyConsolidatedService.Api.Tests
{
    public class ReportsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ReportsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> AuthenticateAsync()
        {
            var registerModel = new RegisterModel { Username = "testuser", Password = "password" };
            var registerContent = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");
            await _client.PostAsync("http://localhost:5001/api/auth/register", registerContent);

            var loginModel = new LoginModel { Username = "testuser", Password = "password" };
            var loginContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("http://localhost:5001/api/auth/login", loginContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenModel = JsonConvert.DeserializeObject<TokenModel>(responseContent);

            return tokenModel.Token;
        }

        [Fact]
        public async Task GetDailyReport_ShouldReturnDailyReport()
        {
            // Arrange
            var token = await AuthenticateAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/reports/daily");
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(responseContent);
        }
    }
}
