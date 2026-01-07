namespace CommentsApi.Resources;

public static class SentimentWords
{
    public static readonly string[] PositiveWords = new[]
    {
        // Adjetivos positivos básicos
        "excelente", "genial", "fantástico", "fantastico", "bueno", "increíble", "increible", "maravilloso",
        "perfecto", "súper", "super", "estupendo", "magnífico", "magnifico", "espléndido", "esplendido", "extraordinario",
        "impresionante", "asombroso", "sorprendente", "increíble", "increible", "fenomenal",
        
        // Calificaciones positivas
        "mejor", "óptimo", "optimo", "ideal", "perfecto", "satisfactorio", "satisfecho",
        "contento", "feliz", "alegre", "encantado", "emocionado", "entusiasmado",
        
        // Expresiones de aprobación
        "recomiendo", "recomendado", "recomendable", "vale la pena", "lo recomiendo",
        "me encanta", "me gusta", "me gustó", "me gusta mucho", "me encantó",
        "me fascina", "me sorprende", "me impresiona",
        
        // Calidad y valor
        "calidad", "buena calidad", "excelente calidad", "alta calidad",
        "valor", "buen precio", "económico", "barato", "accesible",
        "duradero", "resistente", "robusto", "confiable", "confiable",
        
        // Experiencia positiva
        "rápido", "rapido", "rápida", "rapida", "eficiente", "eficaz", "funcional", "práctico", "practico",
        "cómodo", "comodo", "cómoda", "comoda", "fácil", "facil", "sencillo", "intuitivo", "útil", "util",
        "útil", "util", "práctico", "practico", "conveniente",
        
        // Expresiones emocionales positivas
        "amor", "adoro", "adoré", "adore", "me encanta", "me encantó", "me encanto", "me fascina",
        "increíble", "increible", "súper", "super", "genial", "chévere", "chevere", "bacán", "bacan", "padre",
        
        // Comparaciones positivas
        "supera", "superó", "supero", "superar", "superado", "superando", 
        "cumplió", "cumplio", "cumplir", "cumplido", "cumple", "cumplió mis expectativas", "cumplio mis expectativas",
        "mejor que", "mejor de", "superior", "destacado",
        "sobresaliente", "excepcional", "único", "unico", "especial"
    };

    public static readonly string[] NegativeWords = new[]
    {
        // Adjetivos negativos básicos
        "malo", "terrible", "horrible", "defecto", "problema", "pésimo", "pesimo",
        "decepcionante", "frustrante", "molesto", "irritante", "fastidioso",
        "desagradable", "desastroso", "catastrófico", "catastrofico", "inaceptable", "inadecuado",
        
        // Calificaciones negativas
        "peor", "peor que", "inferior", "deficiente", "insuficiente", "limitado",
        "insatisfecho", "disgustado", "molesto", "enojado", "frustrado",
        
        // Expresiones de desaprobación
        "no recomiendo", "no lo recomiendo", "no vale la pena", "no sirve",
        "no funciona", "no me gusta", "no me gustó", "odio", "detesto",
        "me decepciona", "me decepcionó", "me molesta", "me molestó",
        
        // Problemas y defectos
        "defectuoso", "roto", "dañado", "averiado", "mal estado", "en mal estado",
        "falla", "falló", "falla", "error", "errores", "problemas", "dificultades",
        "complicado", "complicaciones", "inconveniente", "inconvenientes",
        
        // Calidad y valor negativos
        "mala calidad", "baja calidad", "calidad inferior", "calidad deficiente",
        "caro", "costoso", "sobreprecio", "no vale", "no vale la pena",
        "frágil", "débil", "quebradizo", "poco duradero", "no dura",
        
        // Experiencia negativa
        "lento", "lenta", "ineficiente", "ineficaz", "no funciona", "no sirve",
        "incómodo", "incomodo", "incómoda", "incomoda", "difícil", "dificil", "complicado", "confuso", "inútil", "inutil",
        "inútil", "inutil", "impráctico", "impractico", "inconveniente",
        
        // Expresiones emocionales negativas
        "odio", "detesto", "me disgusta", "me disgustó", "me molesta",
        "me enoja", "me frustra", "me decepciona", "me decepcionó",
        
        // Comparaciones negativas
        "peor que", "peor de", "inferior a", "no cumple", "no cumple con",
        "no es lo que esperaba", "no es lo esperado", "esperaba más",
        "decepcionante", "decepcionó", "no satisface", "no satisface las expectativas"
    };
}
