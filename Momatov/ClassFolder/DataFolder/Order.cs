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
    
    public partial class Order
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Note { get; set; }
        public int Count { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> ClientID { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual OrderState OrderState { get; set; }
    }
}
