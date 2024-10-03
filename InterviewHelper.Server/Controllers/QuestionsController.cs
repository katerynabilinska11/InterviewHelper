using InterviewHelper.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewHelper.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly AppDbContext _context;
    public QuestionsController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionCard>>> GetQuestionCards()
    {
        return await _context.Questions.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionCard>> GetQuestionCard(int id)
    {
        var questionCard = await _context.Questions.FindAsync(id);

        if (questionCard == null)
            return NotFound();

        return questionCard;
    }
    
    [HttpPost]
    public async Task<ActionResult<QuestionCard>> PostQuestionCard(QuestionCard questionCard)
    {
        _context.Questions.Add(questionCard);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuestionCard), new { id = questionCard.Id }, questionCard);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuestionCard(int id, QuestionCard questionCard)
    {
        if (id != questionCard.Id)
            return BadRequest();

        _context.Entry(questionCard).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!QuestionCardExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestionCard(int id)
    {
        var questionCard = await _context.Questions.FindAsync(id);
        if (questionCard == null)
            return NotFound();

        _context.Questions.Remove(questionCard);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool QuestionCardExists(int id) => _context.Questions.Any(e => e.Id == id);
}