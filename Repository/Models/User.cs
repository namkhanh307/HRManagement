using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int PositionId { get; set; }

    public int SalaryId { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Position Position { get; set; } = null!;

    public virtual Salary Salary { get; set; } = null!;

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
