using System.ComponentModel.DataAnnotations;

namespace CommentsApi.Dtos;

public class CreateCommentDto
{
    [Required]
    public string ProductId { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    [MaxLength(500)]
    public string CommentText { get; set; }
}
