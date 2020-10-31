using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController
    {
        private readonly WebClient client = new WebClient();
        private string start_session_path = "https://paiza-io.p.rapidapi.com/runners/create";
        private string check_session_path = "https://paiza-io.p.rapidapi.com/runners/get_details";
        private Dictionary<string,string> parametrs = new Dictionary<string,string>();
        public struct start_session_answer 
        {
            public string id { get; set; }
            public string status { get; set; }
            public string error { get; set; }
        }
        public struct session_details 
        {
            public string id { get; set; }
            public string language { get; set; }
            public object note { get; set; }
            public string status { get; set; }
            public string build_stdout { get; set; }
            public string build_stderr { get; set; }
            public int build_exit_code { get; set; }
            public string build_time { get; set; }
            public object build_memory { get; set; }
            public string build_result { get; set; }
            public object stdout { get; set; }
            public object stderr { get; set; }
            public object exit_code { get; set; }
            public object time { get; set; }
            public object memory { get; set; }
            public object connections { get; set; }
            public object result { get; set; }
        }
        [HttpPost]
        public string CheckSyntax(string input, string lang, string script)
        {
            var client = new RestClient(start_session_path);
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-rapidapi-host", "paiza-io.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "6db33c3ba2mshaf055bf217d449ap1675c9jsne937bb5a3a9d");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddQueryParameter("language",lang);
            request.AddQueryParameter("input", input);
            request.AddQueryParameter("source_code", script);
            IRestResponse response = client.Execute(request);
            var answer = JsonConvert.DeserializeObject<start_session_answer>(response.Content);
            var result = new session_details();
            bool isCompleted = answer.status == "completed";
            while (!isCompleted)
            {
                if (response.IsSuccessful)
                {
                    client = new RestClient(check_session_path);
                    request = new RestRequest(Method.GET);
                    request.AddQueryParameter("id", answer.id);
                    request.AddHeader("x-rapidapi-host", "paiza-io.p.rapidapi.com");
                    request.AddHeader("x-rapidapi-key", "6db33c3ba2mshaf055bf217d449ap1675c9jsne937bb5a3a9d");
                    response = client.Execute(request);
                    result = JsonConvert.DeserializeObject<session_details>(response.Content);
                    isCompleted = result.status == "completed";
                }
                else return "Что-то пошло не так";
            };
            return result.build_stderr + "\n Код выхода: " + result.build_exit_code;
        }


       
    }
}
