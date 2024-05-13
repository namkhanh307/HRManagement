using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Claim
{
    public string Id { get; set; } = null!;

    public string? Content { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
