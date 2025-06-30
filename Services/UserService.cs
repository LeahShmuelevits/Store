using Entities;
using Repositories;
using Zxcvbn;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _iuserRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository iuserRepository, IConfiguration configuration)
        {
            _iuserRepository = iuserRepository;
            _configuration = configuration;
        }

        public async Task<(User user, string token)> PostLoginS(string username, string password)
        {
            var user = await _iuserRepository.PostLoginR(username, password);
            if (user == null)
                return (null, null);
            var secretKey = _configuration["Jwt:Key"];
            var token = GenerateJwtToken(user, secretKey);
            return (user, token);
        }
        
        public async Task<User> Post(User user)
        {
            int resPassword = CheckPassword(user.Password);
            if (resPassword < 4)
                return null;
            return await _iuserRepository.Post(user);
        }

        public async Task<User> GetById(int id)
        {
            return await _iuserRepository.GetById(id);
        }


        public async Task<User> Put(int id, User user)
        {
            int resPassword = CheckPassword(user.Password);
            if (resPassword < 4)
                return null;
            return await _iuserRepository.Put(id, user);
        }

        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }

        public string GenerateJwtToken(User user, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
