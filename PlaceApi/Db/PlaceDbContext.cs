using Microsoft.EntityFrameworkCore;
using PlaceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceApi.Db
{
    public class PlaceDbContext : DbContext
    {
        public PlaceDbContext(DbContextOptions<PlaceDbContext> options) : base(options) { }
        
        public DbSet<Place> Places { get; set; }
    }
}
