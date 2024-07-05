using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PostingControlService.Api.Models;
using PostingControlService.Application.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PostingControlService.Api.Tests
{
    public class TransactionsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TransactionsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> AuthenticateAsync()
        {
            var registerModel = new RegisterModel { Username = "testuser", Password = "password" };
            var registerContent = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/auth/register", registerContent);

            var loginModel = new LoginModel { Username = "testuser", Password = "password" };
            var loginContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/auth/login", loginContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenModel = JsonConvert.DeserializeObject<TokenModel>(responseContent);

            return tokenModel.Token;
        }

        [Fact]
        public async Task AddTransaction_ShouldReturnOk()
        {
            // Arrange
            var token = await AuthenticateAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var transaction = new TransactionDto { Type = "credit", Amount = 100, Date = System.DateTime.UtcNow };
            var content = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/transactions", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetTransactions_ShouldReturnTransactions()
        {
            // Arrange
            var token = await AuthenticateAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync("/api/transactions");
            var responseContent = await response.Content.ReadAsStringAsync();
            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(responseContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(transactions);
        }
    }
}