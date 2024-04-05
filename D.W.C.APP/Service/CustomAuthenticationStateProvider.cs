using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;

namespace D.W.C.APP.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public void SignIn(string userName, string authenticationType = "CustomScheme")
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName),
            }, authenticationType);

            _currentUser = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void SignOut()
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task SignInWithGoogleAsync(string googleToken)
        {
            var googleTokenVerificationUrl = $"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={googleToken}";
            var response = await _httpClient.GetAsync(googleTokenVerificationUrl);
            if (response.IsSuccessStatusCode)
            {
                var payload = JsonConvert.DeserializeObject<GooglePayload>(await response.Content.ReadAsStringAsync());

                if (payload.aud == "842544829358-uo6j8r74k9ui3cjrqujqmeeiniu4g5al.apps.googleusercontent.com")
                {
                    try
                    {
                        var identity = new ClaimsIdentity(new[]
                        {
                           new Claim(ClaimTypes.Email, payload.email),
                        }, "Google");

                        _currentUser = new ClaimsPrincipal(identity);

                        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Wystąpił wyjątek: {ex.Message}");
                        // Tutaj możesz również logować pełny stos wywołań, jeśli jest to potrzebne.
                    }
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Błąd weryfikacji tokenu Google: {errorContent}");
            }
        }

        public async Task SignInWithMicrosoftAsync(string token)
        {
            // Logika uwierzytelniania za pomocą tokenu Microsoft.
            // Możesz tutaj użyć tokenu, aby zweryfikować użytkownika i ustawić odpowiedni stan uwierzytelnienia.

            // Na przykład, zaktualizuj stan uwierzytelnienia:
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "MicrosoftUserId"),
            // Dodaj inne odpowiednie oświadczenia
        };
            var identity = new ClaimsIdentity(claims, "Microsoft");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private class GooglePayload
        {
            public string imie { get; set; }
            public string sub { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string aud { get; set; }
        }
    }
}