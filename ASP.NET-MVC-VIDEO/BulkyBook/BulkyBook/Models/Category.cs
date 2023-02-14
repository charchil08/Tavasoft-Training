using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name required"), MinLength(2, ErrorMessage = "Category name must be atleast 2 char long")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Display Order")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive values allowed")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}