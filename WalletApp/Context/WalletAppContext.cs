using Microsoft.EntityFrameworkCore;
using WalletApp.Models;

public class WalletAppContext : DbContext
{
	public WalletAppContext(DbContextOptions<WalletAppContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
	public DbSet<DailyPoints> DailyPoints { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).IsRequired();
			entity.Property(e => e.CardBalance).HasColumnType("decimal(18,2)").IsRequired();
			entity.Property(e => e.CardLimit).HasColumnType("decimal(18,2)").IsRequired();
			entity.HasMany(e => e.Transactions).WithOne(e => e.User).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
			entity.HasMany(e => e.DailyPoints).WithOne(e => e.User).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<Transaction>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Type).IsRequired();
			entity.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
			entity.Property(e => e.Name).IsRequired();
			entity.Property(e => e.Description).IsRequired();
			entity.Property(e => e.Date).HasColumnType("date").IsRequired();
			entity.Property(e => e.Pending).IsRequired();
			entity.Property(e => e.AuthorizedUser);
			entity.HasOne(e => e.User).WithMany(e => e.Transactions).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<DailyPoints>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Date).HasColumnType("date").IsRequired();
			entity.Property(e => e.Points).IsRequired();
			entity.HasOne(e => e.User).WithMany(e => e.DailyPoints).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
		});
	}
}
