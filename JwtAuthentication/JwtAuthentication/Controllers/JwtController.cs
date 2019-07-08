using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtAuthentication.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens; /*RG JwtSecurityToken and handler */

namespace JwtAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private APIHelper _helper;
        public JwtController(IOptions<APIHelper> options)
        {
            _helper = options.Value;
        }
        // GET api/jwt
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/jwt/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        
        /// <summary>
        /// RG just to test
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("verify")]
        [AllowAnonymous]
        public string Verify([FromBody]TokenHelper token)
        {
            var jsth = new JwtSecurityTokenHandler();
            if (token!=null && !string.IsNullOrWhiteSpace(token.Token) &&
                jsth.CanReadToken(token.Token))
            {
                var dt = jsth.ReadJwtToken(token.Token);
                return "Success";
            }
            return "Failure";
        }

        [HttpPost("submit")]
        [AllowAnonymous]
        public string Submit([FromBody]User user)
        {
            if (user != null && user.FirstName != null)
            {
                if (user.Screens == null)
                {
                    user.Screens = new List<string>();
                }
                var claims = new[]
                {
                new Claim(ClaimTypes.Name,user.FirstName+" "+user.LastName),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.UserData,user.Screens.Aggregate((inp1,inp2)=>{return inp1+","+inp2; }))
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_helper.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(issuer: "RG", audience: "RG", claims: claims, expires: DateTime.Now.AddHours(24), signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return "Failure";
        }

        // POST api/jwt
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/jwt/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/jwt/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
