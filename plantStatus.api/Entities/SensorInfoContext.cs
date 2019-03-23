using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace plantStatus.api.Entities 
{
    public class SensorInfoContext : DbContext
    {
        public SensorInfoContext(DbContextOptions<SensorInfoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Light> Lights { get; set; }
    }
}
