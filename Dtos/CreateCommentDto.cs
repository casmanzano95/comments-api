public class CreateCommentDto
{
    public required string ProductId { get; set; }
    public required string UserId { get; set; }
    public required string CommentText { get; set; }
}
