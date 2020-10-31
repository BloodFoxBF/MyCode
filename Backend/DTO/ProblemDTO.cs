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
        public virtual ICollection<AssignedProblem> AssignedProblems { get; set; }
    }
}
