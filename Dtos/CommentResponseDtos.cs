public class CommentResponseDto
{
    public int Id { get; set; }
    public string ProductId { get; set; } = "";
    public string UserId { get; set; } = "";
    public string CommentText { get; set; } = "";
    public string Sentiment { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
