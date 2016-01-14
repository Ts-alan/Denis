using System.ComponentModel.DataAnnotations;

namespace Sciencecom.Models
{
    [MetadataType(typeof(OwnerMetaData))]
    public partial class Owner
    {
    }

    
    public class OwnerMetaData
    {
        public int Id { get; set; }
        [Display(Name = "Собственник конструкции")]
        public string Name { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }
    }
}