using CashFlowManagement.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("PostingControlService");
                var loginContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:9001/api/auth/login", loginContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenModel = JsonConvert.DeserializeObject<TokenModel>(responseContent);

                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadJwtToken(tokenModel.Token);
                    var userIdClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "nameid");

                    if (userIdClaim == null)
                    {
                        ModelState.AddModelError(string.Empty, "UserId not found in token.");
                        return View(model);
                    }

                    var userId = userIdClaim.Value;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim("AccessToken", tokenModel.Token),
                        new Claim(ClaimTypes.NameIdentifier, userId)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    HttpContext.Session.SetString("JWToken", tokenModel.Token);
                    HttpContext.Session.SetString("UserId", userId);

                    return RedirectToAction("Index", "Transaction");
                }

                var errorResponse = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Response: " + errorResponse);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
