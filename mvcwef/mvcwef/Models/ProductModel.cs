using System.ComponentModel.DataAnnotations;

namespace mvcwef.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar la descripción.")]
        public string ProductName { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Precio ingresado inválido")]
        public decimal Price { get; set; }
        public long Count { get; set; }
    }
}