using System;
using System.Collections.Generic;

namespace Project3.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? TopicId { get; set; }

    public string? QuestionText { get; set; }

    public string? QuestionType { get; set; }

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual Topic? Topic { get; set; }
}
