using System.ComponentModel.DataAnnotations;

namespace CommentsApi.Dtos;

public class NotEmptyOrWhitespaceAttribute : ValidationAttribute
{
    public NotEmptyOrWhitespaceAttribute()
    {
        ErrorMessage = "El campo no puede estar vacío o contener solo espacios en blanco";
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return false;

        if (value is string str)
        {
            return !string.IsNullOrWhiteSpace(str) && !string.IsNullOrEmpty(str.Trim());
        }

        return true;
    }
}
