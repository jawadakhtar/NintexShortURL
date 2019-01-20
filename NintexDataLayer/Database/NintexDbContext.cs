using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Nintex.BusinessLayer;

namespace Nintex.DatabaseLayer
{
    public class NintexDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<NintexIdentity> NintexIdentity { get; set; }
        public DbSet<URLMap> URLMaps { get; set; }

        public static string ConnectionStringKey { get; private set; }

        public static void Initialize(string aConnectionStringKey)
        {
            ConnectionStringKey = aConnectionStringKey;
        }

        public static NintexDbContext Instance()
        {
            if (string.IsNullOrEmpty(ConnectionStringKey) == true)
            {
                throw new ApplicationException("Nintex DB Context is not initialized. First call the Initialize()");
            }

            return new NintexDbContext(ConnectionStringKey);
        }

        private NintexDbContext(string aConnectionStringKey) : base("name=" + aConnectionStringKey)
        {

        }

    }
}
