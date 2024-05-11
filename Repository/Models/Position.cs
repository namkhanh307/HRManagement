using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Position
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? WorkingLocation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
