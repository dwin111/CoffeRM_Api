

using MauiAppBlazor.Models;
using MauiAppBlazor.Service.Contract;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace MauiAppBlazor.Service
{
    public class CheckService : ICheckService
    {
        private readonly HttpClient _httpClient;

        public CheckService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<Сheck> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Сheck>> GetItems()
        {
            try
            {
                var responce = await _httpClient.GetAsync($"api/Check/GetAll");
                if (responce.IsSuccessStatusCode)
                {
                    if (responce.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(IEnumerable<Сheck>);
                    }
                    return await responce.Content.ReadFromJsonAsync<IEnumerable<Сheck>>();
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

        public async Task<bool> NewCheck(long checkId, long workerId, Dictionary<long, long> ProductsIdAndAmoun)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(ProductsIdAndAmoun), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/Check/NewCheck/{checkId}/{workerId}", stringContent);

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
