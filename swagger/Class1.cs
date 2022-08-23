using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;
namespace ApiVox
{
    internal class ApiVox3i
    {
        private Token token;
        
       public string GetToken(string auth)// получение токена
        {
           RestClient restClient = new RestClient("https://3i-vox.ru/oauth/token");
            
            RestRequest restRequest = new RestRequest();
            restRequest.Method = Method.Post;
            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddBody(auth);
            RestResponse restResponse = restClient.Post(restRequest);
            this.token= JsonSerializer.Deserialize<Token>(restResponse.Content);

            return this.token.access_token;

        }


        public string Profile(string access_token)//информация о пользователе
        {
            RestClient restClient = new RestClient("https://3i-vox.ru/api/v1/profile");

            RestRequest restRequest = new RestRequest();
            restRequest.Method = Method.Get;
            restRequest.AddHeader("accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddHeader("Authorization","Bearer "+access_token);

            RestResponse restResponse = restClient.Get(restRequest);

            return restResponse.Content;
        }

        public void Tts(string voice, string text, string access_token)// tts
        {
            RestClient restClient = new RestClient("https://3i-vox.ru/api/v1/tts/synthesize");

            RestRequest restRequest = new RestRequest();
            restRequest.Method = Method.Post;
            restRequest.AddHeader("accept", "audio/wav");
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + access_token);
            restRequest.AddBody($"{{\"voice\":\"{voice}\",\"text\":\"{text}\",\"speed\":1}}");
            RestResponse restResponse = restClient.Post(restRequest);
            System.IO.File.WriteAllBytes(@"1.wav", restResponse.RawBytes); //путь куда сохранять файл

        }
        
    }

    public class Token
    {
        public string token_type { get;set; }
        public string access_token { get; set; }
        public string scope { get; set; }

        
    }

}
