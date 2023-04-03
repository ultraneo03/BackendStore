using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BackendStore.Mappers
{
    public class ProductDJMapper
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public double price { get; set; }
        public double rating { get; set; }
        public double discountPercentage { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public string category { get; set; }
        public string thumbnail { get; set; }
        public List<string>? images { get; set; }
    }

    public class ProductDJResponseMapper {
        public int total { get; set; }

        public int skip { get; set; }

        public int limit { get; set; }

        public List<ProductDJMapper> products { get; set; }
    }
}
