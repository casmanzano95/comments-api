namespace CommentsApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommentsApi.Models;
using CommentsApi.Services;
using CommentsApi.Data;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ISentimentService _sentimentService;

    public CommentsController(AppDbContext context, ISentimentService sentimentService)
    {
        _context = context;
        _sentimentService = sentimentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentDto dto)
    {
        var sentiment = _sentimentService.Analyze(dto.CommentText);

        var comment = new Comment
        {
            ProductId = dto.ProductId,
            UserId = dto.UserId,
            CommentText = dto.CommentText,
            Sentiment = sentiment
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return Ok(comment);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? productId,
        [FromQuery] string? sentiment)
    {
        var query = _context.Comments.AsQueryable();

        if (!string.IsNullOrEmpty(productId))
            query = query.Where(c => c.ProductId == productId);

        if (!string.IsNullOrEmpty(sentiment))
            query = query.Where(c => c.Sentiment == sentiment);

        var results = await query
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return Ok(results);
    }

    [HttpGet("/api/sentiment-summary")]
    public async Task<IActionResult> GetSummary()
    {
        var total = await _context.Comments.CountAsync();

        var summary = await _context.Comments
            .GroupBy(c => c.Sentiment)
            .Select(g => new
            {
                Sentiment = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        return Ok(new
        {
            Total = total,
            Summary = summary
        });
    }
}
