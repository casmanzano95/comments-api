namespace CommentsApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using CommentsApi.Services;
using CommentsApi.Dtos;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentDto dto)
    {
        // Validar el modelo antes de procesar
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comment = await _commentsService.CreateCommentAsync(dto);
        return Ok(comment);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? productId,
        [FromQuery] string? sentiment)
    {
        var comments = await _commentsService.GetCommentsAsync(productId, sentiment);
        return Ok(comments);
    }

    [HttpGet("/api/sentiment-summary")]
    public async Task<IActionResult> GetSummary()
    {
        var summary = await _commentsService.GetSentimentSummaryAsync();
        return Ok(summary);
    }
}
