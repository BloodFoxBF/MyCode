using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string Description { get; set; }
        public Problem Problem { get; set; }
    }
}
