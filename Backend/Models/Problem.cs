using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Problem
    {
        public int ProblemId { get; set; }
        public string Name { get; set; }
        public string Legend { set; get; }  // Легенда (необязательное поле - предыстория, описывающая подводящая к задаче)

        public string Requirement { set; get; } // условие, описывающее постановку проблемы

        public string Example { set; get; } // примеры (сэмплы) - 1-3 теста, отображающиеся вместе с условием, содержат наборы входных и выходных данных к задаче
        public string FunctionName { get; set; }
        public string FunctionSignature { get; set; }
        public virtual ICollection<AssignedProblem> AssignedProblems { get; set; }

    }
}
