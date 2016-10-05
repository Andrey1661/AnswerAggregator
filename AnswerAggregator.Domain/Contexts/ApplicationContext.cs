using System.Data.Entity;
using System.Linq;
using AnswerAggregator.Domain.Entities;

namespace AnswerAggregator.Domain.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserIdentity> UserIdentities { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; } 
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; } 
        public DbSet<University> Universities { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<GroupSubject> GroupSubjects { get; set; } 

        public ApplicationContext(string connectionString)
            : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasRequired(t => t.Identity)
                .WithRequiredDependent(t => t.Profile)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Subject>()
                .HasMany(t => t.Teachers)
                .WithMany(t => t.Subjects)
                .Map(t => t.MapLeftKey("SubjectId").MapRightKey("TeacherId").ToTable("SubjectTeacher"));

            modelBuilder.Entity<Topic>()
                .HasRequired(t => t.Author)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserMessage>()
                .HasRequired(t => t.Sender)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserMessage>()
                .HasRequired(t => t.Reciever)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
