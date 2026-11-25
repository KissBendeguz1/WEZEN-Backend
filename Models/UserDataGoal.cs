using System;
using System.Collections.Generic;

namespace Wezen_Backend.Models;

public partial class UserDataGoal
{
    public string Id { get; set; } = null!;

    public int? Calories { get; set; }

    public int? Water { get; set; }

    public int? Protein { get; set; }

    public int? Fat { get; set; }

    public int? Carbs { get; set; }

    public int? Fiber { get; set; }

    public int? Sugar { get; set; }

    public int? Sodium { get; set; }

    public int? Colesterol { get; set; }

    public int? Calcium { get; set; }

    public int? Iron { get; set; }

    public int? Potassium { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
