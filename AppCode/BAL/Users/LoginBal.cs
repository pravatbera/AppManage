using AppManage.AppCode.DAL.Users;
using AppManage.Model.System;
using AppManage.Model.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppManage.AppCode.BAL.Users
{
    public class LoginBal
    {
        private readonly LoginDal r;
        private readonly IConfiguration _config;

        public LoginBal(IConfiguration configuration)
        {
            _config = configuration;
            // Pass IConfiguration to MasterDataDal's constructor
            r = new LoginDal(configuration);
        }
        internal User UserAuthentication(User Models)
        {
            //validate entity
            Models.validate();
            //find user
            var xobj = r.UserAuthentication(Models);
            if (xobj != null)
            {
                if (xobj.Password == Models.Password)
                {
                    xobj.Token = GenerateJwtToken(xobj.UserName);
                    return xobj;
                }
                else
                {
                    throw new AuthenticationException("Invalid Password");
                }
            }
            else
            {
                throw new AuthenticationException("Invalid Username or Password");
            }
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
