//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Momatov.ClassFolder.DataFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class Coverage
    {
        public int ID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public decimal Size { get; set; }
        public Nullable<int> Count { get; set; }
    
        public virtual Region Region { get; set; }
    }
}
