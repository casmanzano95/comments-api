using System.Text.RegularExpressions;
using CommentsApi.Resources;

namespace CommentsApi.Services;

public interface ISentimentService
{
    string Analyze(string text);
}

public class SentimentService : ISentimentService
{
    private readonly string[] _positiveWords;
    private readonly string[] _negativeWords;
    private readonly ILogger<SentimentService> _logger;

    public SentimentService(ILogger<SentimentService> logger)
    {
        _positiveWords = SentimentWords.PositiveWords;
        _negativeWords = SentimentWords.NegativeWords;
        _logger = logger;
    }

    public string Analyze(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "neutral";

        var normalizedText = NormalizeText(text);
        var words = ExtractWords(normalizedText);

        var positiveScore = CalculatePositiveScore(words, normalizedText);
        var negativeScore = CalculateNegativeScore(words, normalizedText);

        var sentiment = DetermineSentiment(positiveScore, negativeScore);

        _logger.LogDebug(
            "Análisis de sentimiento: Texto='{Text}', Positivo={PositiveScore}, Negativo={NegativeScore}, Resultado={Sentiment}",
            text.Substring(0, Math.Min(50, text.Length)),
            positiveScore,
            negativeScore,
            sentiment);

        return sentiment;
    }

    private string NormalizeText(string text)
    {
        return text.ToLower().Trim();
    }

    private string[] ExtractWords(string text)
    {
        // Remover signos de puntuación y dividir en palabras
        var cleanedText = Regex.Replace(text, @"[^\w\s]", " ");
        return cleanedText
            .Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(w => w.Length > 2) // Filtrar palabras muy cortas
            .ToArray();
    }

    private int CalculatePositiveScore(string[] words, string fullText)
    {
        var wordScore = words.Count(word => _positiveWords.Contains(word));
        
        // Buscar frases completas positivas (dar más peso)
        var phraseScore = _positiveWords
            .Where(phrase => phrase.Contains(' ') && fullText.Contains(phrase))
            .Count() * 2; // Las frases valen el doble

        return wordScore + phraseScore;
    }

    private int CalculateNegativeScore(string[] words, string fullText)
    {
        var wordScore = words.Count(word => _negativeWords.Contains(word));
        
        // Buscar frases completas negativas (dar más peso)
        var phraseScore = _negativeWords
            .Where(phrase => phrase.Contains(' ') && fullText.Contains(phrase))
            .Count() * 2; // Las frases valen el doble

        return wordScore + phraseScore;
    }

    private string DetermineSentiment(int positiveScore, int negativeScore)
    {
        if (positiveScore > negativeScore)
            return "positive";

        if (negativeScore > positiveScore)
            return "negative";

        // Si hay empate o ambos son 0, verificar si hay alguna palabra clave fuerte
        if (positiveScore == 0 && negativeScore == 0)
            return "neutral";

        // En caso de empate, priorizar negativo (más conservador)
        return "negative";
    }
}
