using AdressbookApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace AdressbookApp.Infrastructure
{
    public class AddressService
    {
        private static HttpClient _client;

        public AddressService()
        {
            _client = new()
            {
                BaseAddress = new Uri("https://localhost:7062/"),
            };
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            using HttpResponseMessage response = await _client.GetAsync("api/Addresses");
            response.EnsureSuccessStatusCode();

            var jsonRepsonse = await response.Content.ReadAsStringAsync();
            var addresses = JsonConvert.DeserializeObject<List<Address>>(jsonRepsonse);
            return addresses ?? new List<Address>();
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            using HttpResponseMessage response = await _client.GetAsync($"api/Addresses/{id}");
            response.EnsureSuccessStatusCode();

            var jsonRepsonse = await response.Content.ReadAsStringAsync();
            var addresses = JsonConvert.DeserializeObject<Address>(jsonRepsonse);
            return addresses ?? new Address();
        }


        public async Task SendAddressAsync(Address address)
        {
            using StringContent jsonContent = new(
                JsonConvert.SerializeObject(address),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = await _client.PostAsync($"api/Addresses/", jsonContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAddressAsync(int id, Address address)
        {
            using StringContent jsonContent = new(
                JsonConvert.SerializeObject(address),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = await _client.PutAsync($"api/Addresses/{id}", jsonContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAddressAsync(int id)
        {
            using HttpResponseMessage response = await _client.DeleteAsync($"api/Addresses/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
