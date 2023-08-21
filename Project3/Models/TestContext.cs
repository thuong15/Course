using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project3.Models;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Centre> Centres { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseDetail> CourseDetails { get; set; }

    public virtual DbSet<Examss> Examsses { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<FeedBack> FeedBacks { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<OrderCourse> OrderCourses { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HHSJ7V0\\SQLEXPRESS;Initial Catalog=Test;\nIntegrated Security=True;TrustServerCertificate=True;\n");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Avatar).HasMaxLength(150);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserName).HasMaxLength(150);
        });

        modelBuilder.Entity<Admission>(entity =>
        {
            entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Englishs).HasMaxLength(10);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Maths).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(12);

            entity.HasOne(d => d.Account).WithMany(p => p.Admissions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Admissions_Accounts");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.BlogImage).HasMaxLength(150);
            entity.Property(e => e.PublishDate).HasColumnType("date");
        });

        modelBuilder.Entity<Centre>(entity =>
        {
            entity.Property(e => e.CentreId).HasColumnName("CentreID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.CentreName).HasMaxLength(250);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(150);
            entity.Property(e => e.Image).HasMaxLength(150);
        });

        modelBuilder.Entity<CourseDetail>(entity =>
        {
            entity.HasKey(e => e.CourseDetailsId);

            entity.Property(e => e.CourseDetailsId).HasColumnName("CourseDetailsID");
            entity.Property(e => e.CourseDetailName).HasMaxLength(150);
            entity.Property(e => e.CourseId).HasColumnName("CourseID");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseDetails_Courses");
        });

        modelBuilder.Entity<Examss>(entity =>
        {
            entity.HasKey(e => e.ExamId);

            entity.ToTable("Examss");

            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Topic).WithMany(p => p.Examsses)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_Examss_Topics");

            entity.HasOne(d => d.User).WithMany(p => p.Examsses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Examss_Accounts");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.ToTable("FAQs");

            entity.Property(e => e.Faqid).HasColumnName("FAQID");
            entity.Property(e => e.Answer).HasColumnType("text");
            entity.Property(e => e.Question).HasColumnType("text");
        });

        modelBuilder.Entity<FeedBack>(entity =>
        {
            entity.ToTable("FeedBack");

            entity.Property(e => e.FeedBackId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(12);
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.Property(e => e.OptionId).HasColumnName("OptionID");
            entity.Property(e => e.OptionText).HasColumnType("text");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Options_Questions");
        });

        modelBuilder.Entity<OrderCourse>(entity =>
        {
            entity.Property(e => e.OrderCourseId).HasColumnName("OrderCourseID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CreatDate).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.OrderCourses)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderCourses_Accounts");

            entity.HasOne(d => d.Course).WithMany(p => p.OrderCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderCourses_Courses");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.QuestionText).HasColumnType("text");
            entity.Property(e => e.QuestionType).HasMaxLength(10);
            entity.Property(e => e.TopicId).HasColumnName("TopicID");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_Questions_Topics");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.Property(e => e.TopicId).HasColumnName("TopicID");
            entity.Property(e => e.CourseDetailsId).HasColumnName("CourseDetailsID");
            entity.Property(e => e.TopicName).HasMaxLength(150);

            entity.HasOne(d => d.CourseDetails).WithMany(p => p.Topics)
                .HasForeignKey(d => d.CourseDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Topics_CourseDetails");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
