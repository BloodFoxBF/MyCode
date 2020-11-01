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
using System;
using Backend.Extensions;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProblemController(ApplicationDbContext context)
        {
            _context = context;
        }

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
        public async Task<string> CheckSyntaxAsync([FromHeader]int id, [FromHeader]string userfunction)
        {
            string lang = "csharp";
            
            var testTable = await _context.GetAllAsync<Test>(x=>x.Problem.ProblemId == id);
            Problem problem = JsonConvert.DeserializeObject<Problem>(await GetProblem(id));
            string funcname = problem.FunctionName;
            string tests = "";
            foreach (var row in testTable) 
            {
                if (tests.Length != 0) 
                {
                    tests += ";";
                }
                tests += row.Description;
            }
            var testCasesString = tests.Split(';');
            var testCasesDictionary = new Dictionary<int, string>();
            int key = 0;
            foreach (var testcase in testCasesString)
            {
                var splitData = testcase.Split(',').ToList();
                var outputData = splitData.Last<string>();
                string inputData = splitData.Aggregate((x, y) => x +","+ y).Remove(splitData.Count());
                testCasesDictionary.Add(key, inputData+":"+outputData);
                key++;
            }

            string code = (System.IO.File.ReadAllText("Controllers\\Template.txt"));
            
            code = code.Replace("@func", userfunction);
            var testcodestring = "Console.WriteLine(\"Вход - @input, Ожидалось - @expected, Получилось - \"+@actual);";
            var testingresult = "";
            foreach (var test in testCasesDictionary)
            {
                var InputAndOutput = test.Value.Split(':');
                testingresult += testcodestring.Replace("@input", "[" + InputAndOutput[0] + "]")
                                               .Replace("@actual", funcname+"(" + InputAndOutput[0] + ")")
                                               .Replace("@expected", InputAndOutput[1]) + 
                                               "if ("+ funcname + "(" + InputAndOutput[0] + ") != " + InputAndOutput[1] + ") {Console.Write(\" - Ошибка.\");}";
            }
            code = code.Replace("@call", testingresult);

            var client = new RestClient(start_session_path);
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-rapidapi-host", "paiza-io.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "6db33c3ba2mshaf055bf217d449ap1675c9jsne937bb5a3a9d");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddQueryParameter("language",lang);
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

            return result.build_stderr + "\nКод выхода: " + result.build_exit_code + "\nВывод: "+ result.stdout;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<string> GetProblems()
        {
            //Минус подхода в том, что он формирует петли, следовательно, мы не можем автоматически мапить данные в DTO структуры.
            var problemsList = await _context.GetAllAsync<Problem>();
            List<ProblemDTO> problemDTOs = new List<ProblemDTO>();

            foreach (var problem in problemsList)
            {
                var problemDTO = new ProblemDTO()
                {
                    ProblemId = problem.ProblemId,
                    Name = problem.Name,
                    Example = problem.Example,
                    Requirement = problem.Requirement,
                    Legend = problem.Legend
                };
                problemDTOs.Add(problemDTO);
            }

            return JsonConvert.SerializeObject(problemDTOs);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<string> GetProblem(int id)
        {
            var problem = await _context.Problems.FindAsync(id);

            if (problem == null)
            {
                return "";
            }

            return JsonConvert.SerializeObject(problem);
        }

        

    }
}
