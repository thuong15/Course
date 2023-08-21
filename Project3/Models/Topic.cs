using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    public string? TopicName { get; set; }

    public int CourseDetailsId { get; set; }

    public virtual CourseDetail CourseDetails { get; set; } = null!;

    public virtual ICollection<Examss> Examsses { get; set; } = new List<Examss>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
