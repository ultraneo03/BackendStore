using System.ComponentModel.DataAnnotations;

namespace BackendStore.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Product
    {
        /// <summary>
        /// product identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// product name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// product category
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// product category
        /// </summary>
        [Required]
        public string Category { get; set; }
        /// <summary>
        /// product price
        /// </summary>
        [Required]
        public double Price { get; set; }
        /// <summary>
        /// product rating
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// product stock
        /// </summary>
        [Required]
        public int Stock { get; set; }
        /// <summary>
        /// product image
        /// </summary>
        [Required]
        public string? Image { get; set; }

        public List<string>? Images { get; set; }
    }
}
