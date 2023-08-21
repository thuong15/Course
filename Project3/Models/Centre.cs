using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Centre
{
    public int CentreId { get; set; }

    public string? CentreName { get; set; }

    public string? Address { get; set; }

    public string? Telephone { get; set; }
}
