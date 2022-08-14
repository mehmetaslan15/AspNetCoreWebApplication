using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApplication.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        [Display(Name = "Marka Adı"), StringLength(50), Required(ErrorMessage = "Marka Adı Boş Geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Marka Açıklama"), DataType(DataType.MultilineText)]  // Description inputunun yerine textarea olması için. Textbox tek satır, textarea çok satır girilmesine imkan verir. 
        public string? Description { get; set; }
        [Display(Name = "Marka Logosu"), StringLength(50)]
        public string? Logo { get; set; }
        public ICollection<Product> Products { get; set; }
        public Brand()
        {
            Products = new List<Product>();
        }
    }
}
