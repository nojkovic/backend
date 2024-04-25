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
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var payload = new[]
            {
                new Claim("id",user.Id.ToString()),
                new Claim("name",user.Name),
                new Claim("email",user.Email),
                new Claim("lastName",user.LastName)
            };

            var jwtToken = new JwtSecurityToken(
                issuer:"localhost",
                audience:"localhost",
                claims:payload,
                expires:DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials:signature
                
                
                );
            var response = new JwtSecurityTokenHandler().WriteToken(jwtToken) ;
            return response;
           
        }
        #endregion

    }
}
