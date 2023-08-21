using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class CourseDetail
{
    public int CourseDetailsId { get; set; }

    public int CourseId { get; set; }

    public string? CourseDetailName { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
