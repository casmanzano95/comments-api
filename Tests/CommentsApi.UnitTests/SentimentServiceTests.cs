using CommentsApi.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace CommentsApi.UnitTests;

public class SentimentServiceTests
{
    private readonly ISentimentService _service;

    public SentimentServiceTests()
    {
        // Usamos NullLogger para no depender del logger real
        var logger = new NullLogger<SentimentService>();
        _service = new SentimentService(logger);
    }

    [Fact]
    public void Analyze_PositiveText_ReturnsPositive()
    {
        // Texto de prueba
        string text = "excelente cumplio mis expectativas";

        // Ejecutar método
        var result = _service.Analyze(text);

        // Verificar resultado
        Assert.Equal("positive", result);
    }

    [Fact]
    public void Analyze_NegativeText_ReturnsNegative()
    {
        string text = "horrible, no me gustó";

        var result = _service.Analyze(text);

        Assert.Equal("negative", result);
    }

    [Fact]
    public void Analyze_NeutralText_ReturnsNeutral()
    {
        string text = "hola";

        var result = _service.Analyze(text);

        Assert.Equal("neutral", result);
    }
}
