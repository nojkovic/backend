using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Models
{
    public class JWTServices
    {
        #region Data
        public String SecretKey { get; set; }
        public int TokenDuration { get; set; }
        private readonly IConfiguration config;
        #endregion

        #region Construct
        public JWTServices(IConfiguration _config)
        {
            config = _config;
            this.SecretKey = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);

        }
        #endregion
        #region Methods
        public object GenerateToken(String id,String Name,String Email,String LastName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var payload = new[]
            {
                new Claim("id",id),
                new Claim("name",Name),
                new Claim("email",Email),
                new Claim("lastName",LastName)
            };

            var jwtToken = new JwtSecurityToken(
                issuer:"localhost",
                audience:"localhost",
                claims:payload,
                expires:DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials:signature
                
                
                );
            var response = new { token=new JwtSecurityTokenHandler().WriteToken(jwtToken) ,status="log"};
            return response;
           
        }
        #endregion

    }
}
