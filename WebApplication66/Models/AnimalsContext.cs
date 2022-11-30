using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication66.Models
{
    public class AnimalsContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<SkinColor> SkinColors { get; set; }
        public DbSet<KindOfAnimal> KindOfAnimals { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Region> Regions { get; set; }

        public AnimalsContext(DbContextOptions<AnimalsContext> options): base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
