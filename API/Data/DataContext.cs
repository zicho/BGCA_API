using API.Data.Entities;
using API.Data.Entities.Messaging;
using API.Data.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserFriendship> UserFriendships { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<PrivateMessage> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriendship>()
                .HasKey(e => new { e.UserId, e.User2Id });

            modelBuilder.Entity<UserFriendship>()
                .HasOne(e => e.User)
                .WithMany(e => e.Friends)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserFriendship>()
                .Ignore(c => c.Id)
                .HasOne(e => e.User2);
        }
    }
}