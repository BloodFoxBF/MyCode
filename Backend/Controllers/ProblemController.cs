using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">Входные данные</param>
        /// <param name="lang">Язык</param>
        /// <param name="userfunction">Функция пользователя</param>
        /// <param name="tests">Тестовые случаи</param>
        /// <returns></returns>
        [HttpPost]
        public string CheckSyntax(string input, string lang, string userfunction, string tests)
        {
            #region tests

            #endregion
            var testcases = tests.Split(';');
            var testCasesDictionary = new Dictionary<string, string>();
            testCasesDictionary.Clear();
            foreach (var testcase in testcases)
            {
                var splitData = testcase.Split(',').ToList();
                var outputData = splitData.Last<string>();
                string inputData = splitData.Aggregate((x, y) => x +","+ y).Remove(splitData.Count());
                testCasesDictionary.Add(inputData, outputData);
            }

            string code = (File.ReadAllText("Controllers\\Template.txt"));
            code = code.Replace("@call", "");
            code = code.Replace("@func", userfunction);
            

            var client = new RestClient(start_session_path);
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-rapidapi-host", "paiza-io.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "6db33c3ba2mshaf055bf217d449ap1675c9jsne937bb5a3a9d");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddQueryParameter("language",lang);
            request.AddQueryParameter("input", input);
            request.AddQueryParameter("source_code", code);
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
            code = (File.ReadAllText("Controllers\\Template.txt"));
            code = code.Replace("@func", userfunction);
            
            var testcodestring = "Console.WriteLine(\"Ожидалось - @expected, Получилось - \"+@actual);";
            var testingresult = "";
            foreach (var test in testCasesDictionary) 
            {
                testingresult += testcodestring.Replace("@actual", "action("+test.Key+")").Replace("@expected", test.Value);
            }

            code = code.Replace("@call", testingresult);
            client = new RestClient(start_session_path);
            request = new RestRequest(Method.POST);
            request.AddHeader("x-rapidapi-host", "paiza-io.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "6db33c3ba2mshaf055bf217d449ap1675c9jsne937bb5a3a9d");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddQueryParameter("language", lang);
            request.AddQueryParameter("input", input);
            request.AddQueryParameter("source_code", code);
            response = client.Execute(request);
            answer = JsonConvert.DeserializeObject<start_session_answer>(response.Content);
            result = new session_details();
            isCompleted = answer.status == "completed";
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


            return result.build_stderr + "\nКод выхода: " + result.build_exit_code + "\nВывод: "+ result.stdout;
        }


       
    }
}
