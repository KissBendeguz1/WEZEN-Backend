using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Wezen_Backend.Controllers.Dtos;
using Wezen_Backend.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.RegularExpressions;
using MySqlX.XDevAPI.Common;

namespace Wezen_Backend.Controllers
{
    [Route("/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly WezenContext _context;

        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginController(WezenContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        [HttpPost("newUser")]
        public async Task<IActionResult> RegisterUser([FromBody] NewUserDto userDto)
        {
            try
            {
                // Check if the username or email already exists
                if (await _context.Users.AnyAsync(u => u.Username == userDto.Username || u.Email == userDto.Email))
                {
                    return Conflict("Username or email already exists.");
                }



                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = userDto.Name,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    RoleId = 1,
                    Salt = string.Empty,
                };

                Regex rgx = new Regex(@"(?:[a-z0-9!#$%&'*+\x2f=?^_\x7b-\x7d~\x2d]+(?:\.[a-z0-9!#$%&'*+\x2f=?^_\x7b-\x7d~\x2d]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9\x2d]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9\x2d]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9\x2d]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
                if (rgx.IsMatch(userDto.Email))
                {
                    newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();
                    return StatusCode(200, "Sikeres Regisztracio");
                }
                else
                {
                    return StatusCode(404, "Az email hibas");
                };
                


            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = $"Hiba: {ex.Message}" });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginDto logindto)
        {
            try
            {

                string username = logindto.Username;
                string password = logindto.Password;
               
                if (string.IsNullOrWhiteSpace(logindto.Username))
                {
                    return StatusCode(404, "A felhasználónév megadása kötelező.");
                }

                if (string.IsNullOrWhiteSpace(logindto.Password))  
                {
                    return StatusCode(404, "A jelszó megadása kötelező.");
                }

                var user = await _context.Users.SingleOrDefaultAsync(a => a.Username == username);

                if (user == null)
                {
                    return StatusCode(404, "Hibás felhasználónév");
                }
                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, logindto.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    return StatusCode(404, "Hibás  jelszó.");
                }
                return StatusCode(200, "Sikeres Bejelentkezes");
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = $"Hiba: {ex.Message}" });
            }
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                if (users != null)
                {
                    return StatusCode(200, users);
                }
                return StatusCode(404, "Nincsenek felhasznalok mentve.");
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = $"Hiba: {ex.Message}" });
            }


        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (id == null)
                {
                    return StatusCode(404, "A keresett user ne talalhato");

                }
                return StatusCode(200, "A keresett user megtalalva.");
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = $"Hiba: {ex.Message}" });
            }


        }
    }
}
