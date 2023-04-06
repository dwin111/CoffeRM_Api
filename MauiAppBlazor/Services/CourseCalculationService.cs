using MauiAppBlazor.Data;
using MauiAppBlazor.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBlazor.Services
{
    public class CourseCalculationService: ICourseCalculationService
    {
        private readonly HttpClient _httpClient = new HttpClient();


        public async Task<List<Сourse>> GetСourseAsync()
        {
            try
            {
                var responce = await _httpClient.GetAsync("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");
                if (responce.IsSuccessStatusCode)
                {
                    if(responce.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(List<Сourse>);
                    }
                    return await responce.Content.ReadFromJsonAsync<List<Сourse>>();
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


    }
}
