using AutoMapper;
using BackendStore.Interfaces;
using BackendStore.Mappers;
using BackendStore.Models;
using System.Collections.Generic;
using System.Net.Http;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BackendStore.Services
{
    public class DummyJsonClient : IStoreClient
    {
        public string BaseUrl;
        private readonly MapperConfiguration MapperConfig;
        private static DummyJsonClient instance;

        public DummyJsonClient()
        {
            BaseUrl = "https://dummyjson.com";
            MapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDJMapper, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.images.FirstOrDefault()))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.rating))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.images));
            });
        }

        public static DummyJsonClient Instance
        {
            get
            {
                if (instance == null)
                    instance = new DummyJsonClient();

                return instance;
            }
        }
        string IStoreClient.BaseUrl => BaseUrl;

        public async Task<List<Product>> getProducts(int page = 1, string? search = null, int size = 50, string? order = null)
        {
            var client = new HttpClient();

            string url = $"{BaseUrl}/products/";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"search?q={search}";
            }
            
            if (size != 0)
            {
                int skip = ((page*size)-size);
                url += url.Contains("?") ? $"&limit={size}&skip={skip}" : $"?limit={size}&skip={skip}";
            }

            var response = await client.GetAsync(url);

            var mapper = MapperConfig.CreateMapper();

            List<Product> products = new List<Product>();
            if (response.IsSuccessStatusCode)
            {
                var productsDJ = await response.Content.ReadFromJsonAsync<ProductDJResponseMapper>();

                products = mapper.Map<List<Product>>(productsDJ.products);
            }

            if (!string.IsNullOrEmpty(order))
            {
                switch (order)
                {
                    case "price":
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    case "price_desc":
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                    case "name":
                        products = products.OrderBy(x => x.Name).ToList();
                        break;
                    case "name_desc":
                        products = products.OrderByDescending(x => x.Name).ToList();
                        break;
                }
            }

            return products;
        }

        public async Task<Product> getProductDetailDyID(int id)
        {
            var client = new HttpClient();

            string url = $"{BaseUrl}/products/{id}";

            var response = await client.GetAsync(url);

            var mapper = MapperConfig.CreateMapper();

            Product product = new Product();
            if (response.IsSuccessStatusCode)
            {
                var productDJ = await response.Content.ReadFromJsonAsync<ProductDJMapper>();

                product = mapper.Map<Product>(productDJ);
            }

            return product;
        }

    }
}
