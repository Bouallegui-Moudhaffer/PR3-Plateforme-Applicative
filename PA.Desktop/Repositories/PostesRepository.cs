using Azure;
using Newtonsoft.Json;
using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace PA.Desktop.Repositories
{
    public class PostesRepository : BaseRepository
    {
        public PostesRepository(HttpClient httpClient) : base(httpClient) { }

        public async Task<IEnumerable<Postes>> GetAllPostes()
        {
            var reponse = await GetAsync<IEnumerable<Postes>>("api/Postes");
            return reponse;
        }
        public async Task UpdatePostes(int postId, Postes postes)
        {
            string uri = $"api/Postes/{postId}";
            await PutAsync(uri, postes);
        }

        public async Task UpdateIpAddress(int postId, string ipAddress)
        {
            string uri = $"api/Postes/{postId}/UpdateIpAddress";

            await PutAsync(uri, ipAddress);
        }
        public async Task<HttpResponseMessage> SendAggregatedDataToApi(int postId, AggregatedDataModel aggregatedData)
        {
            string uri = $"api/Postes/{postId}/UpdateAggregatedData";
            var response = await PutAsync(uri, aggregatedData);

            return response;
        }
    }
}
