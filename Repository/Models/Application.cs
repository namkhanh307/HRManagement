using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Application
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Type { get; set; }

    public string? FileAttach { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
