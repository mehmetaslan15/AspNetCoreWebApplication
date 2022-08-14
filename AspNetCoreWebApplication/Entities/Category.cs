using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApplication.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı"), StringLength(50), Required(ErrorMessage = "Kategori Adı Boş Geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklama"), DataType(DataType.MultilineText)]  // Description inputunun yerine textarea olması için. Textbox tek satır, textarea çok satır girilmesine imkan verir. 
        public string? Description { get; set; }
        [Display(Name = "Kategori Resmi"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]  // ScaffoldColumn(false) : sayfa oluştururken bu kolon oluşmasın
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
