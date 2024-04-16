using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trilha_net_api_desafio.Models;

namespace trilha_net_api_desafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase {
        private readonly OrganizerContext _context;

        public TarefaController(OrganizerContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Tasks tasks) {
            if (tasks == null) {
                return BadRequest(new {Erro = "Tarefa inválida"});
            }

            if (tasks.Date == DateTime.MinValue) {
                return BadRequest(new {Erro = "Data inválida"});
            }

            if (tasks.Status != EnumTaskStatus.Pending && tasks.Status != EnumTaskStatus.Finished) {
                return BadRequest(new {Erro = "Status inválido"});
            }

            _context.Add(tasks);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = tasks.Id }, tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null) {
                return NotFound();
            }

            return Ok(tasks);
        }
    
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Tasks tasks) {
            var tasksExists = await _context.Tasks.FindAsync(id);
            if (tasksExists == null) {
                return NotFound();
            }

            if (tasks.Date == DateTime.MinValue) {
                return BadRequest(new {Erro = "Data inválida"});
            }

            if (tasks.Status != EnumTaskStatus.Pending && tasks.Status != EnumTaskStatus.Finished) {
                return BadRequest(new {Erro = "Status inválido"});
            }

            tasksExists.Title = tasks.Title;
            tasksExists.Description = tasks.Description;
            tasksExists.Date = tasks.Date;
            tasksExists.Status = tasks.Status;

            _context.Tasks.Update(tasksExists);
            await _context.SaveChangesAsync();
            return Ok(tasksExists);
        }
    
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null) {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> GetAll() {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }
    
        [HttpGet("ObterPorStatus")]
        public async Task<IActionResult> GetByStatus(EnumTaskStatus status) {
            if (status != EnumTaskStatus.Pending && status != EnumTaskStatus.Finished) {
                return BadRequest(new {Erro = "Status inválido"});
            }

            var tasks = await _context.Tasks.Where(x => x.Status == status).ToListAsync();
            return Ok(tasks);
        }
    
        [HttpGet("ObterPorData")]
        public async Task<IActionResult> GetByDate(DateTime date) {
            var tasks = await _context.Tasks.Where(x => x.Date.Date == date.Date).ToListAsync();
            return Ok(tasks);
        }
    
        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> GetByTitle(string title) {
            var tasks = await _context.Tasks.Where(x => x.Title == title).ToListAsync();
            return Ok(tasks);
        }
    }
}