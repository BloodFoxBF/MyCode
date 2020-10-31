using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ProblemDTO
    {
        public int ProblemId { get; set; }
        public string Name { get; set; }
        public string Legend { set; get; }
        public string Requirement { set; get; }
        public string Example { set; get; }
        public virtual ICollection<AssignedProblem> AssignedProblems { get; set; }
    }
}
