using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.W.C.APP.Service
{
    public class AuthenticationService
    {
        public bool IsUserAuthenticated { get; private set; }

        // Metoda do aktualizacji stanu autoryzacji
        public void UpdateAuthenticationState(bool isAuthenticated)
        {
            IsUserAuthenticated = isAuthenticated;
        }

        
    }
}
