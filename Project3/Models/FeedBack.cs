using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class FeedBack
{
    public int FeedBackId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Message { get; set; }
}
