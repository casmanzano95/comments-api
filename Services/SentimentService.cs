using System.Globalization;
using System.Text;
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
    private readonly string[] _positivePhrases;
    private readonly string[] _negativePhrases;
    private readonly ILogger<SentimentService> _logger;

    public SentimentService(ILogger<SentimentService> logger)
    {
        // Separar palabras de frases
        _positiveWords = SentimentWords.PositiveWords.Where(w => !w.Contains(' ')).ToArray();
        _negativeWords = SentimentWords.NegativeWords.Where(w => !w.Contains(' ')).ToArray();
        _positivePhrases = SentimentWords.PositiveWords.Where(w => w.Contains(' ')).ToArray();
        _negativePhrases = SentimentWords.NegativeWords.Where(w => w.Contains(' ')).ToArray();

        _logger = logger;
    }

    public string Analyze(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "neutral";

        // Normalizar texto: minúsculas + quitar acentos
        var normalizedText = NormalizeText(text);

        // Extraer palabras del texto
        var words = ExtractWords(normalizedText);

        // Calcular puntajes
        int positiveScore = CountMatches(words, normalizedText, _positiveWords, _positivePhrases);
        int negativeScore = CountMatches(words, normalizedText, _negativeWords, _negativePhrases);

        // Determinar sentimiento final
        string sentiment = DetermineSentiment(positiveScore, negativeScore);

        // Log breve para depuración
        var preview = text.Length > 50 ? text.Substring(0, 50) + "..." : text;
        _logger.LogDebug("Texto='{Text}', Positivo={PositiveScore}, Negativo={NegativeScore}, Resultado={Sentiment}",
            preview, positiveScore, negativeScore, sentiment);

        return sentiment;
    }

    private string NormalizeText(string text)
    {
        text = text.ToLower().Trim();
        return RemoveAccents(text);
    }

    private string RemoveAccents(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;

        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    private string[] ExtractWords(string text)
    {
        var cleaned = Regex.Replace(text, @"[^\w\s]", " "); // quitar signos de puntuación
        return cleaned.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                      .Where(w => w.Length > 2)
                      .ToArray();
    }

    private int CountMatches(string[] words, string fullText, string[] wordList, string[] phraseList)
    {
        // Contar palabras que coincidan exactamente
        int wordScore = words.Count(w => wordList.Contains(w));

        // Contar frases que aparezcan en el texto completo (doble puntaje)
        int phraseScore = phraseList.Count(p => fullText.Contains(p)) * 2;

        return wordScore + phraseScore;
    }

    private string DetermineSentiment(int positiveScore, int negativeScore)
    {
        if (positiveScore > negativeScore) return "positive";
        if (negativeScore > positiveScore) return "negative";

        // Empate o sin coincidencias
        return positiveScore == 0 && negativeScore == 0 ? "neutral" : "positive";
    }
}
