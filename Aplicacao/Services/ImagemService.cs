
using Aplicacao.Interfaces;
using Imgur.API;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Aplicacao.Services
{
    public class ImagemService : IImagemService
    {
        private readonly string _clientId;
        private readonly HttpClient _httpClient;

        public ImagemService(string clientId, HttpClient httpClient)
        {
            _clientId = clientId;
            _httpClient = httpClient;
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            using var fileStream = imageFile.OpenReadStream();
            using var content = new StreamContent(fileStream);

            content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);

            var response = await _httpClient.PostAsync("https://api.imgur.com/3/image", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseString);
            return responseObject["data"]["link"].ToString();
            /*var client = new ApiClient(_clientId);
            var imageEndpoint = new ImageEndpoint(client, _httpClient);
            using var fileStream = imageFile.OpenReadStream();
            var image = await imageEndpoint.UploadImageAsync(fileStream);
            return image.Link;*/
        }

    }
}
