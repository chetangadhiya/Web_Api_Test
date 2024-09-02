using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace Web_Api_Test.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        Demo1Entities db = new Demo1Entities();  
        
        [Route("Login")]
        [HttpPost]
        // GET: Account
        public IHttpActionResult Login(SignUp user)
        {
            var userName = user.UserName;
            var password = user.Password;

            var UserData = db.Users.Where(x => x.UserName.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();
            

            if (UserData != null)
            {
                var getToken = GetToken(user);
                return Ok(getToken);
            }
            return Unauthorized();
            
            
        }

        public object GetToken(User user)
        {

            String key = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
            var issuer = "https://localhost:44358/";
            var audience = "https://localhost:44358/";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("UserID", "1"));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwtToken };





        }
    }
}