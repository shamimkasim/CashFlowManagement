using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PostingControlService.Api;
using PostingControlService.Api.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PostingControlService.Api.Tests
{
    public class AuthControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AuthControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_ShouldReturnOk()
        {
            // Arrange
            var model = new RegisterModel { Username = "testuser", Password = "password" };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/auth/register", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Login_ShouldReturnToken()
        {
            // Arrange
            var registerModel = new RegisterModel { Username = "testuser", Password = "password" };
            var registerContent = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/auth/register", registerContent);

            var loginModel = new LoginModel { Username = "testuser", Password = "password" };
            var loginContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/auth/login", loginContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenModel = JsonConvert.DeserializeObject<TokenModel>(responseContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(tokenModel.Token);
        }
    }
}