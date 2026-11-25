using System;
using System.Collections.Generic;

namespace Wezen_Backend.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string Salt { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual UserDataGoal? UserDataGoal { get; set; }

    public virtual UserDatum? UserDatum { get; set; }
}
