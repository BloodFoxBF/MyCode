using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Extensions;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignedProblemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssignedProblemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AssignedProblems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignedProblemDTO>>> GetAssignedTasks(int studentID, bool isSolved, DateTime startDate, DateTime finishDate)
        {
            var assignedProblemList = await _context.GetAllAsync<AssignedProblem>(x => x.StudentId == studentID && x.isSolved == isSolved && (x.StartDate >= startDate && x.StartDate < finishDate));
            List<AssignedProblemDTO> assignedProblemDTOs = new List<AssignedProblemDTO>();
            foreach (var assignedProblem in assignedProblemList)
            {
                var assignedProblemDTO = new AssignedProblemDTO()
                {
                    StudentId = assignedProblem.StudentId,
                    ProblemId = assignedProblem.ProblemId,
                    Mark = assignedProblem.Mark
                };
                assignedProblemDTOs.Add(assignedProblemDTO);
            }

            return new ActionResult<IEnumerable<AssignedProblemDTO>>(assignedProblemDTOs);
        }

        // GET: api/AssignedProblems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignedProblem>> GetAssignedProblem(int id)
        {
            var assignedProblem = await _context.AssignedTasks.FindAsync(id);

            if (assignedProblem == null)
            {
                return NotFound();
            }

            return assignedProblem;
        }

        // PUT: api/AssignedProblems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignedProblem(int id, AssignedProblem assignedProblem)
        {
            if (id != assignedProblem.AssignedProblemId)
            {
                return BadRequest();
            }

            _context.Entry(assignedProblem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignedProblemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AssignedProblems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AssignedProblem>> PostAssignedProblem(AssignedProblem assignedProblem)
        {
            _context.AssignedTasks.Add(assignedProblem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssignedProblem", new { id = assignedProblem.AssignedProblemId }, assignedProblem);
        }

        // DELETE: api/AssignedProblems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssignedProblem>> DeleteAssignedProblem(int id)
        {
            var assignedProblem = await _context.AssignedTasks.FindAsync(id);
            if (assignedProblem == null)
            {
                return NotFound();
            }

            _context.AssignedTasks.Remove(assignedProblem);
            await _context.SaveChangesAsync();

            return assignedProblem;
        }

        private bool AssignedProblemExists(int id)
        {
            return _context.AssignedTasks.Any(e => e.AssignedProblemId == id);
        }
    }
}
