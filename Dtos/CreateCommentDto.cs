using System.ComponentModel.DataAnnotations;

namespace CommentsApi.Dtos;

public class CreateCommentDto
{
    [Required(ErrorMessage = "El ProductId es requerido")]
    [NotEmptyOrWhitespace(ErrorMessage = "El ProductId no puede estar vacío o contener solo espacios en blanco")]
    public string ProductId { get; set; }

    [Required(ErrorMessage = "El UserId es requerido")]
    [NotEmptyOrWhitespace(ErrorMessage = "El UserId no puede estar vacío o contener solo espacios en blanco")]
    public string UserId { get; set; }

    [Required(ErrorMessage = "El texto del comentario es requerido")]
    [NotEmptyOrWhitespace(ErrorMessage = "El texto del comentario no puede estar vacío o contener solo espacios en blanco")]
    [MaxLength(500)]
    public string CommentText { get; set; }
}
