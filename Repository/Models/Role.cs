using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
