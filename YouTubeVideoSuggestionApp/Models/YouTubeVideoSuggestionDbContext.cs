using Microsoft.EntityFrameworkCore;

namespace YouTubeVideoSuggestionApp.Models;

public partial class YouTubeVideoSuggestionDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public YouTubeVideoSuggestionDbContext(DbContextOptions<YouTubeVideoSuggestionDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }
    public virtual DbSet<Suggestion> Suggestions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Suggestion>(entity =>
        {
            entity.ToTable("Suggestion");

            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(65)
                .IsUnicode(false);
            entity.Property(e => e.ShortDescription).HasMaxLength(50);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Username).HasMaxLength(25);
            entity.Property(e => e.YouTubeUrl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("YouTubeURL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
