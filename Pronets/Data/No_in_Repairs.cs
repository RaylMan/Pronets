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
    
    public partial class No_in_Repairs
    {
        public int RepairId { get; set; }
        public string Nomenclature { get; set; }
        public string Serial_Number { get; set; }
        public int ClientId { get; set; }
        public string Identifie_Fault { get; set; }
        public string Work_Done { get; set; }
        public string Repair_Category { get; set; }
        public int Engineer { get; set; }
        public Nullable<System.DateTime> Repair_Date { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}
