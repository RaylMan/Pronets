﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pronets.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PronetsDataBaseEntities : DbContext
    {
        public PronetsDataBaseEntities()
            : base("name=PronetsDataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<No_in_Repairs> No_in_Repairs { get; set; }
        public virtual DbSet<Nomenclature> Nomenclature { get; set; }
        public virtual DbSet<Nomenclature_Types> Nomenclature_Types { get; set; }
        public virtual DbSet<Parts> Parts { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<ReceiptDocument> ReceiptDocument { get; set; }
        public virtual DbSet<Repair_Categories> Repair_Categories { get; set; }
        public virtual DbSet<Repairs> Repairs { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<v_Receipt_Document> v_Receipt_Document { get; set; }
    }
}
