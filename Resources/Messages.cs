namespace CommentsApi.Resources;

public static class Messages
{
    public const string CommentCannotBeNull = "El comentario no puede ser nulo";
    public const string CommentTextRequired = "El texto del comentario es requerido";
    public const string CommentTextCannotBeEmpty = "El texto del comentario no puede estar vacío o contener solo espacios en blanco";
    public const string ProductIdRequired = "El ProductId es requerido";
    public const string ProductIdCannotBeEmpty = "El ProductId no puede estar vacío o contener solo espacios en blanco";
    public const string UserIdRequired = "El UserId es requerido";
    public const string UserIdCannotBeEmpty = "El UserId no puede estar vacío o contener solo espacios en blanco";
    public const string InvalidSentiment = "El sentimiento debe ser: positive, negative o neutral";
    
    public static string CommentCreatedSuccessfully(int commentId, string productId) =>
        $"Comentario creado exitosamente. Id: {commentId}, ProductId: {productId}";
}
