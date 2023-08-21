using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class OrderCourse
{
    public int OrderCourseId { get; set; }

    public int CourseId { get; set; }

    public int AccountId { get; set; }

    public DateTime? CreatDate { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
