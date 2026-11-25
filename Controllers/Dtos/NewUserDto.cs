using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wezen_Backend.Controllers.Dtos
{
    public class NewUserDto
    {
        public string Name { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
