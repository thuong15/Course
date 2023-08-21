using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using Microsoft.Build.Framework;


namespace Project3.Models;

public partial class Account
{
    public int UserId { get; set; }
	[Required(ErrorMessage = "UserName is required.")]
	public string? UserName { get; set; }

	[Required(ErrorMessage = "Password is required.")]
	//[StringLength(8, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
	public string? Password { get; set; }

    public int? RoleId { get; set; }
	[Required(ErrorMessage = "FullName is required.")]
	public string? FullName { get; set; }
	[Required(ErrorMessage = "Email is required.")]
	public string? Email { get; set; }
	
	public DateTime? Birthday { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }
	[Required(ErrorMessage = "Phone is required.")]
	[StringLength(10, ErrorMessage = "Password must be at least 10 characters long.", MinimumLength = 10)]
	public string? Phone { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();

    public virtual ICollection<Examss> Examsses { get; set; } = new List<Examss>();

    public virtual ICollection<OrderCourse> OrderCourses { get; set; } = new List<OrderCourse>();
}
