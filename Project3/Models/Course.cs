using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public string? CourseDescription { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<CourseDetail> CourseDetails { get; set; } = new List<CourseDetail>();

    public virtual ICollection<OrderCourse> OrderCourses { get; set; } = new List<OrderCourse>();
}
