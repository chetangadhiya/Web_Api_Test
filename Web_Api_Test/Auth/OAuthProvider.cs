using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Web_Api_Test.Auth
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.CompletedTask;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using(var db = new Demo1Entities())
            {
                var username = context.UserName;
                var password = context.Password;

                var user = db.Users.Where(x => x.UserName.Equals(username) && x.Password.Equals(password)).FirstOrDefault();

                var roles = db.UserRoles.Where(x => x.UserID == user.UserID).ToList();
                if(user != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("Age", "24"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    foreach (var item in roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, item.Role.RoleName));
                    }
                    context.Validated(identity);
                    
                }
                else
                {
                    context.SetError("Invalid", "Invalid username and password");
                    context.Rejected();

                }
            }
        }
    }
}