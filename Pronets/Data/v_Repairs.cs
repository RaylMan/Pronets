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
    
    public partial class v_Repairs
    {
        public int RepairId { get; set; }
        public Nullable<int> DocumentId { get; set; }
        public string Nomenclature { get; set; }
        public string Serial_Number { get; set; }
        public string Claimed_Malfunction { get; set; }
        public int Client_Id { get; set; }
        public string Client_Name { get; set; }
        public Nullable<System.DateTime> Date_Of_Receipt { get; set; }
        public Nullable<System.DateTime> Departure_Date { get; set; }
        public int InspectorId { get; set; }
        public string Inspector { get; set; }
        public string Warranty { get; set; }
        public string Identifie_Fault { get; set; }
        public string Work_Done { get; set; }
        public string Repair_Category { get; set; }
        public int EngineerId { get; set; }
        public string Engineer { get; set; }
        public Nullable<System.DateTime> Repair_Date { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Recipient { get; set; }
    }
}
