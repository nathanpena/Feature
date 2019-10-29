
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Web.Configuration;
using System.Web.Http;
using Features.Models;
using Features.Providers;

using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Data.Entity;

namespace Features.Controllers
{

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private string _sessionDuration = WebConfigurationManager.AppSettings["SESSION_DURATION_MINUTES"];
        private readonly ILoginProvider _loginProvider;
        private UTRGVAppContext db = new UTRGVAppContext();

        public AccountController(ILoginProvider loginProvider)
        {
            _loginProvider = loginProvider;
        }

        [HttpPost, Route("Login")]
        public async Task<IHttpActionResult> Login(UTRGVCredentials login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ClaimsIdentity identity;
            string cn;
            bool authorized = false;
            if (!_loginProvider.ValidateCredentials(login.email, login.password, out cn, out authorized))
            {
                return BadRequest("Incorrect user or password");
            }
            if (!authorized)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You're not authorized"));
            }

            //set the identity values
            identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, cn));

            var dbUser = await db.Users.Where(u => u.Cn == cn).FirstOrDefaultAsync();

            if (dbUser != null)
                identity.AddClaim(new Claim(ClaimTypes.Role, dbUser.Role.Name));
            else
                identity.AddClaim(new Claim(ClaimTypes.Role, "Faculty"));



            var duration = int.Parse(_sessionDuration);
            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(duration));



            return Ok(Startup.OAuthOptions.AccessTokenFormat.Protect(ticket));
        }
    }
}
