using BackendStore.Models;

namespace BackendStore.Interfaces
{
    public interface IProductsPagination
    {
        int totalPages { get; set; }
        IEnumerable<Product> items { get; set; }
    }
}
