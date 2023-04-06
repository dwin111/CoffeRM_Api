using MauiAppBlazor.Models;
using MauiAppBlazor.Service.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiAppBlazor.Service
{
    public class ProductCatalogService: IProductCatalogService
    {
        private readonly HttpClient _httpClient;

        public ProductCatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ProductCatalog> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductCatalog>> GetItems()
        {
            try
            {
                var responce = await _httpClient.GetAsync($"api/ProductCatalog/GetAll");
                if (responce.IsSuccessStatusCode)
                {
                    if (responce.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(IEnumerable<ProductCatalog>);
                    }
                    return await responce.Content.ReadFromJsonAsync<IEnumerable<ProductCatalog>>();
                }
                else
                {
                    var message = await responce.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Create(ProductCatalogDto productViewModel)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(productViewModel), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/ProductCatalog/NewProduct", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
