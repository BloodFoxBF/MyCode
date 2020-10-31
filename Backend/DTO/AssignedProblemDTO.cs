using Backend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Backend.Models
{
    public class AssignedProblemDTO
    {
        public int AssignedProblemId { get; set; }
        public int StudentId { get; set; }
        public int ProblemId { get; set; }
        public StudentDTO Student { get; set; }
        public ProblemDTO Problem { get; set; }
    }
}
