using Microsoft.EntityFrameworkCore;
using OnlineChat.Models;

namespace OnlineChat.Repository
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }
        
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForNpgsqlUseIdentityColumns();
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(x => x.Name); 
                
            });
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Chat)
                    .WithMany(x => x.Messages)
                    .HasForeignKey(x => x.ChatName);
                entity.HasOne(x => x.User)
                    .WithMany(x => x.Messages)
                    .HasForeignKey(x => x.UserId);
            });
            modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(x => x.Id);
                    entity.HasOne(x => x.Chat)
                        .WithMany(x => x.Users)
                        .HasForeignKey(x => x.ChatName);
                }
            );
        }
    }
}