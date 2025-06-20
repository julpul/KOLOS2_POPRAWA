using KOLOS2.Models;
using Microsoft.EntityFrameworkCore;

namespace KOLOS2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    
    public DatabaseContext(){}
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new Item(){ItemId = 1,Name = "item1",Weight = 10},
            new Item(){ItemId = 2,Name = "item2",Weight = 10},
            new Item(){ItemId = 3,Name = "item3",Weight = 10}
        });
        
        modelBuilder.Entity<Character>().HasData(new List<Character>()
        {
            new Character(){CharacterId = 1,FirstName = "jul",LastName = "pow",CurrentWeight = 120,MaxWeight = 125},
            new Character(){CharacterId = 2,FirstName = "jula",LastName = "powa",CurrentWeight = 90,MaxWeight = 95},
        });
        
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>()
        {
            new Backpack(){CharacterId = 1,ItemId = 1,Amount = 2},
            new Backpack(){CharacterId = 2,ItemId = 2,Amount = 2},
            new Backpack(){CharacterId = 2,ItemId = 3,Amount = 2},
        });
        modelBuilder.Entity<Title>().HasData(new List<Title>()
        {
            new Title(){TitleId = 1,Name = "Title 1"},
            new Title(){TitleId = 2,Name = "Title 2"},
        });
        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>()
        {
            new CharacterTitle(){CharacterId = 1,TitleId = 1,AcquiredAt = DateTime.Parse("2025-11-11")},
            new CharacterTitle(){CharacterId = 2,TitleId = 2,AcquiredAt = DateTime.Parse("2025-11-11")},
        });
    }
}