namespace CommentsApi.Services;
public interface ISentimentService
{
    string Analyze(string text);
}

public class SentimentService : ISentimentService
{
    private readonly string[] PositiveWords =
        { "excelente", "genial", "fantástico", "bueno", "increíble" };

    private readonly string[] NegativeWords =
        { "malo", "terrible", "horrible", "defecto", "problema" };

    public string Analyze(string text)
    {
        var lower = text.ToLower();

        if (PositiveWords.Any(w => lower.Contains(w)))
            return "positive";

        if (NegativeWords.Any(w => lower.Contains(w)))
            return "negative";

        return "neutral";
    }
}
