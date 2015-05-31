using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sciencecom.Models
{
    public partial class Surface
    {
        public string SideOfSurface { get; set; }
        public HttpPostedFileBase SeveralPhoto{ get; set;}

        public Guid Id_Bilboard { get; set; }
    }
    public partial class Billboards1
    {
        public string OwnerName { get; set; }

    }
}