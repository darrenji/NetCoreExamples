using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.Infrastructure.Security
{
    public class BasicAuthentiationHanler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthentiationHanler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorizaiton Headers");
            }

            string validatedLogin;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':'},2);

                var username = credentials[0];
                var password = credentials[1];
                validatedLogin = (username == "admin" && password == "admin") ? "admin" : null;
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            if(validatedLogin==null)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            var claims = new[] { 
                new Claim(ClaimTypes.NameIdentifier, validatedLogin),
                new Claim(ClaimTypes.Name, validatedLogin)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var priincipal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(priincipal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
                
        }
    }
}
