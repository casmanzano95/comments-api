using CommentsApi.Controllers;
using CommentsApi.Data;
using CommentsApi.Models;
using CommentsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace CommentsApi.IntegrationTests;

public class CommentsControllerTests
{
    private readonly CommentsController _controller;
    private readonly AppDbContext _context;

    public CommentsControllerTests()
    {
        // Crear DbContext en memoria
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);

        // Servicio de sentimiento
        var sentimentService = new SentimentService(new NullLogger<SentimentService>());

        // Controller
        _controller = new CommentsController(_context, sentimentService);
    }

    [Fact]
    public async Task Create_AddsComment()
    {
        var dto = new CreateCommentDto
        {
            ProductId = "p1",
            UserId = "u1",
            CommentText = "excelente producto"
        };

        var result = await _controller.Create(dto) as OkObjectResult;
        var comment = result!.Value as Comment;

        Assert.NotNull(comment);
        Assert.Equal("positive", comment!.Sentiment);
        Assert.Equal(dto.CommentText, comment.CommentText);
    }

    [Fact]
    public async Task GetAll_FiltersByProduct()
    {
        // Añadir datos
        _context.Comments.Add(new Comment { ProductId = "p1", UserId = "u1", CommentText = "bien", Sentiment = "positive" });
        _context.Comments.Add(new Comment { ProductId = "p2", UserId = "u2", CommentText = "mal", Sentiment = "negative" });
        await _context.SaveChangesAsync();

        var result = await _controller.GetAll(productId: "p1", sentiment: null) as OkObjectResult;
        var comments = result!.Value as List<Comment>;

        Assert.Single(comments);
        Assert.Equal("p1", comments![0].ProductId);
    }
}
