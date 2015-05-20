using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sciencecom.Models
{
    [MetadataType(typeof(OwnerMetaData))]
    public partial class Owner
    {
    }

    
    public class OwnerMetaData
    {
        public int Id { get; set; }
        [Display(Name = "Владелец")]
        public string Name { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }
    }
}