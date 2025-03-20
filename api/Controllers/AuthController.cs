using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BCrypt.Net;
using api.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    /// <summary>
    /// Controller for handling user authentication operations like registration, login, and user profile.
    /// </summary>
    [ApiController]// This attribute is used to mark the controller as an API controller
    [Route("api/[controller]")]// This attribute is used to specify the route for the controller
    public class AuthController : ControllerBase
    {
        // This is the constructor for the AuthController class
        private readonly ApplicationDbContext _context;
        // This is the configuration for the AuthController class
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="config">The application configuration.</param>
        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="dto">The registration data.</param>
        /// <returns>User information if registration is successful.</returns>
        [HttpPost("register")]// This attribute is used to specify the route for the register method
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            // This is the register method for the AuthController class
            // Check if the email is already in use
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                // If the email is already in use, return a bad request
                return BadRequest("Email already in use.");
            }

            // Hash the user's password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User
            {
                Email = dto.Email,
                Password = hashedPassword, // Hash the user's password
                FullName = dto.FullName, // Set the user's full name
                ProfilePicture = dto.ProfilePicture, // Set the user's profile picture
                Bio = dto.Bio // Set the user's bio
            };

            _context.Users.Add(user); // Add the user to the database
            await _context.SaveChangesAsync(); // Save the changes to the database
            
            // Return UserDto instead of full User object
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName ?? string.Empty,
                ProfilePicture = user.ProfilePicture,
                Bio = user.Bio
            };


            return Ok(userDto);
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="dto">The login credentials.</param>
        /// <returns>JWT token and user information if login is successful.</returns>
        [HttpPost("login")] // This attribute is used to specify the route for the login method
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            // This is the login method for the AuthController class
            // Check if the user exists
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
            // If the user does not exist, return an unauthorized message
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Return UserDto with the token
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName ?? string.Empty,
                ProfilePicture = user.ProfilePicture,
                Bio = user.Bio
            };

            // Return the JWT token and user data
            return Ok(new { Token = token, User = userDto });
        }

        /// <summary>
        /// Gets the current authenticated user's profile information.
        /// </summary>
        /// <returns>The user profile information.</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            // Get the current user ID from the claims
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName ?? string.Empty,
                ProfilePicture = user.ProfilePicture,
                Bio = user.Bio
            };

            return userDto;
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate a token for.</param>
        /// <returns>A JWT token string.</returns>
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // Get the key for the JWT token
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            // Create a token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Create a token descriptor for the JWT token
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Add the user's ID as a claim
                    new Claim(ClaimTypes.Email, user.Email) // Add the user's email as a claim
                    // Add more claims if needed (e.g., roles)  
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Set the expiration time for the JWT token (7 days)
                Issuer = _config["Jwt:Issuer"], // Set the issuer for the JWT token
                Audience = _config["Jwt:Audience"], // Set the audience for the JWT token
                // Set the signing credentials for the JWT token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create the JWT token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Write the JWT token to a string
            return tokenHandler.WriteToken(token);
        }
    }

    /// <summary>
    /// Data Transfer Object for user registration requests.
    /// </summary>
    public class UserRegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for user login requests.
    /// </summary>
    public class UserLoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
