using System;
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
                entity.Property(x => x.Name).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

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
                    entity.Property(x => x.Id).ValueGeneratedOnAdd();

                    entity.HasOne(x => x.Chat)
                        .WithMany(x => x.Users)
                        .HasForeignKey(x => x.ChatName);
                }
            );
            SeedDb(modelBuilder);
        }

        private static void SeedDb(ModelBuilder modelBuilder)
        {
            var chatDevelopers = new Chat{ Name = "Developers"};
            var chatDesigners = new Chat{Name = "Designers"};
                
            var userValera = new User{ Id = 1, Name = "Valera", ChatName = chatDevelopers.Name};
            var userMax = new User {Id = 2, Name = "Max", ChatName = chatDevelopers.Name};
            var userTom = new User {Id = 3, Name = "Tom", ChatName = chatDevelopers.Name};

            var messageFromValera = new Message {Id = 1, Text = "Hey there", ChatName = chatDevelopers.Name, UserId = userValera.Id, CTime = DateTime.Now};
            var messageFromMax = new Message {Id = 2, Text = "What's up", ChatName = chatDevelopers.Name, UserId = userMax.Id, CTime = DateTime.Now};

            modelBuilder.Entity<Chat>().HasData(chatDevelopers, chatDesigners);
            modelBuilder.Entity<User>().HasData(userValera, userMax, userTom);
            modelBuilder.Entity<Message>().HasData(messageFromValera, messageFromMax);
        }
    }
}