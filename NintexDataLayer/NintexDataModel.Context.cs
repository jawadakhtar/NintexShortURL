﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nintex.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NintexUrlDbEntities : DbContext
    {
        public NintexUrlDbEntities()
            : base("name=NintexUrlDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<URL> URLs { get; set; }
        public virtual DbSet<VWClientPackage> VWClientPackages { get; set; }
        public virtual DbSet<VWClientURL> VWClientURLs { get; set; }
    }
}