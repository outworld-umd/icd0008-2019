using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Choice> Choices { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Answer> Answers { get; set; } = default!;

        public AppDbContext(DbContextOptions options) : base(options) {}
    }
}