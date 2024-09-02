using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web_Api_Test.Auth;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[assembly: OwinStartup(typeof(Web_Api_Test.Startup1))]

namespace Web_Api_Test
{
    public class Startup1
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public string PublicClientId { get; private set; }


        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44358/",
                    ValidAudience = "https://localhost:44358/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR=="))
                }
            });
           


            //Below code is for OAuth
            //PublicClientId = "self";
            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new Microsoft.Owin.PathString("/Token"),
            //    Provider = new OAuthProvider(),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
            //};

            //app.UseOAuthAuthorizationServer(OAuthOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            //HttpConfiguration configuration = new HttpConfiguration();
            //WebApiConfig.Register(configuration);
        }
    }
}
