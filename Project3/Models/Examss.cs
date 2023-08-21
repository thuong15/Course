using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Examss
{
    public int ExamId { get; set; }

    public int UserId { get; set; }

    public int? TopicId { get; set; }

    public int? Point { get; set; }

    public virtual Topic? Topic { get; set; }

    public virtual Account User { get; set; } = null!;
}
