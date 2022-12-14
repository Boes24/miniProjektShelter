using System;
using System.Net.Http.Json;
using miniProjektShelter.Shared;

namespace miniProjektShelter.Client.Services{
    public class ShelterService : IShelterService{

        private readonly HttpClient httpClient;

        public ShelterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Shelter> GetItem(string id)
        {
            var result = await httpClient.GetFromJsonAsync<Shelter>("api/shelterAPI/" + id);
            return result!;
        }

        public Task<Shelter[]?> GetAllItems()
        {
            //var result = httpClient.GetFromJsonAsync<ShoppingItem[]>("sample-data/shoppingdata.json");
            var result = httpClient.GetFromJsonAsync<Shelter[]>("api/shelterAPI");
            return result;
        }

        public Task<CustomerBooking[]?> GetAllBookings()
        {
            //var result = httpClient.GetFromJsonAsync<ShoppingItem[]>("sample-data/shoppingdata.json");
            var result = httpClient.GetFromJsonAsync<CustomerBooking[]>("api/shelterAPI/getAllBookings");
            return result;
        }

        public async Task<int> AddItem(CustomerBooking costumerInfo)
        {
            var response = await httpClient.PostAsJsonAsync("api/shelterAPI", costumerInfo);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<int> DeleteItem(string id)
        {
            var response = await httpClient.DeleteAsync("api/shelterAPI" + id);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<int> UpdateItem(CustomerBooking item)
        {
            Console.WriteLine("UpdateItem med booking");
            var response = await httpClient.PutAsJsonAsync("api/shelterAPI", item);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;

        }
        
    }
}

