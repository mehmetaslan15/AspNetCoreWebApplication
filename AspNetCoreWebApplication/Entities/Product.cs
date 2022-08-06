using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApplication.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Ürün Adı"), StringLength(50), Required(ErrorMessage = "Ürün Adı Boş Geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Ürün Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Ürün Resmi"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi")]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }    //Classlar arasında bağlantıları kurduk. Category ile product arasında mesela bağlantı oluşturduk bu kodla
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }   
        
    }
}
