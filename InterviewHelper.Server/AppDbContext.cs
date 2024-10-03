using InterviewHelper.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewHelper.Server;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<QuestionCard> Questions { get; set; }
}