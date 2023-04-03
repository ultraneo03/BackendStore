using BackendStore.Models;

namespace BackendStore.Interfaces
{
    public interface IStoreClient
    {
        string BaseUrl { get; }
        Task<List<Product>> getProducts(int page = 1,string? search = null, int size = 50, string? order = null);
        Task<Product> getProductDetailDyID(int id);
    }
}
