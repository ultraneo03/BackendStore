using AutoMapper;
using BackendStore.Interfaces;
using BackendStore.Mappers;
using BackendStore.Models;
using System.Collections.Generic;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BackendStore.Services
{
    public class FakeStoreClient : IStoreClient
    {
        public string BaseUrl;
        private readonly MapperConfiguration MapperConfig;
        private static FakeStoreClient instance;
        protected FakeStoreClient()
        {
            BaseUrl = "https://fakestoreapi.com";
            MapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductFSMapper, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => 10))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.rating.rate));
                
            }); 
        }

        public static FakeStoreClient Instance
        {
            get { 
                if(instance == null)
                    instance = new FakeStoreClient();

                return instance; 
            }
        }

        string IStoreClient.BaseUrl => BaseUrl;

        public async Task<List<Product>> getProducts(int page = 1, string? search = null, int size = 50, string? order = null)
        {
            var client = new HttpClient();

            string url = $"{BaseUrl}/products/";

            var response = await client.GetAsync(url);

            var mapper = MapperConfig.CreateMapper();

            List<Product> products = new List<Product>();
            if (response.IsSuccessStatusCode)
            {
                var productsFS = await response.Content.ReadFromJsonAsync<List<ProductFSMapper>>();

                products = mapper.Map<List<Product>>(productsFS);
            }

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(x =>  x.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            if (size != 0)
            {
                products = products.Take(page * size).ToList() ;
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
                var productFS = await response.Content.ReadFromJsonAsync<ProductFSMapper>();

                product = mapper.Map<Product>(productFS);
            }

            return product;
        }
    }
}
