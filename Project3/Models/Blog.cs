using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? Title { get; set; }

    public string? Contents { get; set; }

    public string? BlogImage { get; set; }

    public DateTime? PublishDate { get; set; }
}
