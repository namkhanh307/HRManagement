using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Application
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Type { get; set; }

    public string? FileAttach { get; set; }

    public Guid? UserId { get; set; }

    public virtual User? User { get; set; }
}
