using Microsoft.Identity.Client;

namespace D.W.C.APP.Service
{
    internal class MicrosoftAuthService
    {
        private IPublicClientApplication _msalClient;
        private string[] _scopes = new string[] { "User.Read" }; // Dostosuj zakresy zgodnie z wymaganiami
        private string _clientId = ""; // Podaj Client ID aplikacji zarejestrowanej w Azure AD
        private string _tenantId = ""; // Podaj Tenant ID

        public MicrosoftAuthService()
        {
        }

        public async Task<AuthenticationResult> AuthenticateAsync()
        {
            try
            {
                // Najpierw próbujemy uzyskać token z pamięci podręcznej
                var accounts = await _msalClient.GetAccountsAsync();
                var firstAccount = accounts.FirstOrDefault();
                var silentResult = await _msalClient.AcquireTokenSilent(_scopes, firstAccount).ExecuteAsync();

                return silentResult;
            }
            catch (MsalUiRequiredException)
            {
                // Jeśli nie możemy uzyskać tokena z pamięci podręcznej, wykonujemy interaktywne zapytanie
                try
                {
                    var interactiveResult = await _msalClient.AcquireTokenInteractive(_scopes).ExecuteAsync();
                    return interactiveResult;
                }
                catch (Exception ex)
                {
                    // Obsługa błędów logowania
                    Console.WriteLine($"An error occurred during interactive authentication: {ex.Message}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Ogólna obsługa błędów
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}