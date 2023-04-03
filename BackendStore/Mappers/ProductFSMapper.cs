using System.ComponentModel.DataAnnotations;

namespace BackendStore.Mappers
{
    public class rating {
        public double rate { get; set; }

        public int count { get; set; }
    }
    public class ProductFSMapper
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public double price { get; set; }
        public string description { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string image { get; set; }

        public rating rating { get; set; }
    }
}
