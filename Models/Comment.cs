namespace CommentsApi.Models;
public class Comment
{
    public int Id { get; set; }

    public required string ProductId { get; set; }

    public required string UserId { get; set; }

    public required string CommentText { get; set; }

    public required string Sentiment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
