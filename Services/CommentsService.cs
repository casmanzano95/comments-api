using Microsoft.EntityFrameworkCore;
using CommentsApi.Data;
using CommentsApi.Models;
using CommentsApi.Dtos;
using CommentsApi.Resources;

namespace CommentsApi.Services;

public interface ICommentsService
{
    Task<Comment> CreateCommentAsync(CreateCommentDto dto);
    Task<IEnumerable<Comment>> GetCommentsAsync(string? productId, string? sentiment);
    Task<object> GetSentimentSummaryAsync();
}

public class CommentsService : ICommentsService
{
    private readonly AppDbContext _context;
    private readonly ISentimentService _sentimentService;
    private readonly ILogger<CommentsService> _logger;
    private static readonly string[] ValidSentiments = { "positive", "negative", "neutral" };

    public CommentsService(
        AppDbContext context,
        ISentimentService sentimentService,
        ILogger<CommentsService> logger)
    {
        _context = context;
        _sentimentService = sentimentService;
        _logger = logger;
    }

    public async Task<Comment> CreateCommentAsync(CreateCommentDto dto)
    {
        ValidateCreateCommentDto(dto);
        
        var sentiment = AnalyzeSentiment(dto.CommentText);
        var comment = BuildComment(dto, sentiment);
        
        await SaveCommentAsync(comment);
        
        LogCommentCreated(comment);
        
        return comment;
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(string? productId, string? sentiment)
    {
        ValidateSentiment(sentiment);
        
        var query = BuildCommentsQuery(productId, sentiment);
        var results = await ExecuteQueryAsync(query);
        
        return results;
    }

    public async Task<object> GetSentimentSummaryAsync()
    {
        var total = await GetTotalCommentsCountAsync();
        var summary = await GetSentimentGroupedSummaryAsync();
        
        return new
        {
            Total = total,
            Summary = summary
        };
    }

    private void ValidateCreateCommentDto(CreateCommentDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), Messages.CommentCannotBeNull);

        if (string.IsNullOrWhiteSpace(dto.CommentText))
            throw new ArgumentException(Messages.CommentTextRequired, nameof(dto.CommentText));

        if (string.IsNullOrWhiteSpace(dto.ProductId))
            throw new ArgumentException(Messages.ProductIdRequired, nameof(dto.ProductId));

        if (string.IsNullOrWhiteSpace(dto.UserId))
            throw new ArgumentException(Messages.UserIdRequired, nameof(dto.UserId));
    }

    private string AnalyzeSentiment(string commentText)
    {
        return _sentimentService.Analyze(commentText);
    }

    private Comment BuildComment(CreateCommentDto dto, string sentiment)
    {
        return new Comment
        {
            ProductId = dto.ProductId,
            UserId = dto.UserId,
            CommentText = dto.CommentText,
            Sentiment = sentiment
        };
    }

    private async Task SaveCommentAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    private void LogCommentCreated(Comment comment)
    {
        _logger.LogInformation(Messages.CommentCreatedSuccessfully(comment.Id, comment.ProductId));
    }

    private void ValidateSentiment(string? sentiment)
    {
        if (string.IsNullOrWhiteSpace(sentiment))
            return;

        if (!ValidSentiments.Contains(sentiment.ToLower()))
            throw new ArgumentException(Messages.InvalidSentiment, nameof(sentiment));
    }

    private IQueryable<Comment> BuildCommentsQuery(string? productId, string? sentiment)
    {
        var query = _context.Comments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(productId))
            query = ApplyProductIdFilter(query, productId);

        if (!string.IsNullOrWhiteSpace(sentiment))
            query = ApplySentimentFilter(query, sentiment);

        return query;
    }

    private IQueryable<Comment> ApplyProductIdFilter(IQueryable<Comment> query, string productId)
    {
        return query.Where(c => c.ProductId == productId);
    }

    private IQueryable<Comment> ApplySentimentFilter(IQueryable<Comment> query, string sentiment)
    {
        return query.Where(c => c.Sentiment.ToLower() == sentiment.ToLower());
    }

    private async Task<List<Comment>> ExecuteQueryAsync(IQueryable<Comment> query)
    {
        return await query
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    private async Task<int> GetTotalCommentsCountAsync()
    {
        return await _context.Comments.CountAsync();
    }

    private async Task<List<object>> GetSentimentGroupedSummaryAsync()
    {
        var result = await _context.Comments
            .GroupBy(c => c.Sentiment)
            .Select(g => new
            {
                Sentiment = g.Key,
                Count = g.Count()
            })
            .ToListAsync();
        
        return result.Cast<object>().ToList();
    }
}
