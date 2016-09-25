using System.Data.Entity;
using AnswerAggregator.Domain.Entities;

namespace AnswerAggregator.Domain.Contexts
{
    public class EfContext : DbContext
    {
        public IDbSet<UserProfile> UserProfiles { get; set; }

        public IDbSet<UserIdentity> UserIdentities { get; set; }

        public IDbSet<University> Universities { get; set; }

        public IDbSet<Institute> Institutes { get; set; }

        public IDbSet<Department> Departments { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public EfContext(string connectionString)
            : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasRequired(t => t.Identity)
                .WithRequiredDependent(t => t.Profile)
                .WillCascadeOnDelete(true);
        }
    }
}
