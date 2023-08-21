using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Admission
{
    public int AdmissionId { get; set; }

    public int AccountId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public DateTime Birthday { get; set; }

    public string? Maths { get; set; }

    public string? Englishs { get; set; }

    public virtual Account Account { get; set; } = null!;
}
