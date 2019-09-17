//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Repairs
    {
        public int RepairId { get; set; }
        public Nullable<int> DocumentId { get; set; }
        public string Nomenclature { get; set; }
        public string Serial_Number { get; set; }
        public string Claimed_Malfunction { get; set; }
        public Nullable<int> Client { get; set; }
        public Nullable<System.DateTime> Date_Of_Receipt { get; set; }
        public Nullable<System.DateTime> Departure_Date { get; set; }
        public Nullable<int> Inspector { get; set; }
        public string Warranty { get; set; }
        public string Identifie_Fault { get; set; }
        public string Work_Done { get; set; }
        public string Repair_Category { get; set; }
        public Nullable<int> Engineer { get; set; }
        public Nullable<System.DateTime> Repair_Date { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Nomenclature Nomenclature1 { get; set; }
        public virtual ReceiptDocument ReceiptDocument { get; set; }
        public virtual Repair_Categories Repair_Categories { get; set; }
        public virtual Statuses Statuses { get; set; }
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
        public virtual Engineers Engineers { get; set; }
    }
}
