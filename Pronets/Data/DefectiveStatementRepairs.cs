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
    
    public partial class DefectiveStatementRepairs
    {
        public int Id { get; set; }
        public int DefectiveStatementId { get; set; }
        public int RepairId { get; set; }
    
        public virtual DefectiveStatements DefectiveStatements { get; set; }
        public virtual Repairs Repairs { get; set; }
    }
}