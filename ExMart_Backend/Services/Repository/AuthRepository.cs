using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExMart_Backend.Data;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ExMart_Backend.Services.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IConfiguration _configuration;
        private string SecretKey;

        public AuthRepository(IConfiguration configuration, ApplicationDBContext db)
        {
            _db = db;
            _configuration = configuration;
            SecretKey = _configuration.GetValue<string>("ApiSettings:Secret");
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginReq)
        {
           
                // Validate input
                if (string.IsNullOrEmpty(loginReq.Email))
                {
                    throw new ArgumentException("Email cannot be null or empty");
                }

                // Find user with error handling
                var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == loginReq.Email);
                if (user == null)
                {

                    return null;
                }

                bool isAdmin = await _db.AdminMembers.AnyAsync(m => m.UserId == user.Id);

                // Get JWT settings
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];

                if (string.IsNullOrEmpty(secretKey))
                {
                    throw new InvalidOperationException("JWT secret key is not configured");
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User"),
                    new Claim("UserId", user.Id.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                return new LoginResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Username = user.Name
                };
            }
        
    }
}
    
