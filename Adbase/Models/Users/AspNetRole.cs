//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sciencecom.Models.Users
{
    using System;
    using System.Collections.Generic;
    
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            this.AspNetUsers = new HashSet<AspNetUser>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
