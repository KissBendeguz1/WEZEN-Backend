using System;
using System.Collections.Generic;

namespace Wezen_Backend.Models;

public partial class UserDatum
{
    public string Id { get; set; } = null!;

    public int? DailyCalories { get; set; }

    public int? DailyWater { get; set; }

    public int? DailyProtein { get; set; }

    public int? DailyFat { get; set; }

    public int? DailyCarbs { get; set; }

    public int? DailyFiber { get; set; }

    public int? DailySugar { get; set; }

    public int? DailySodium { get; set; }

    public int? DailyColesterol { get; set; }

    public int? DailyCalcium { get; set; }

    public int? DailyIron { get; set; }

    public int? DailyPotassium { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
