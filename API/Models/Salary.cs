using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Salary
{
    public int Id { get; set; }

    public double? FixedAmount { get; set; }

    public int? OnFurlough { get; set; }

    public int? Absent { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
