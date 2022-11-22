using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options ) : base(options)
        {
            
        }

        //This triggers a compiler nullable warning
        //public DbSet<Character> Characters {get; set;}
        public DbSet<Character> Characters  => Set<Character>();

    }
}