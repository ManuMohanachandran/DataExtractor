using Sprache;

public static class StringParserUtility
{
    private static Parser<char> _delimiter = Parse.Chars("|;");
    private static Parser<char> _assignment = Parse.Char(':');
    private static Parser<string> _value = Parse.AnyChar.Except(_delimiter).Many().Text();
    private static Parser<string> _keyParser = Parse.AnyChar.Except(_delimiter).Except(_assignment).Many().Text();
    private static Parser<string> GenerateKeyValueParser(string key)
    {
        return
            from a in Parse.AnyChar.Until(Parse.String(key).Text()).Text()
            from k in _keyParser
            from d in _assignment
            from v in _value
            select v;
    }
    private static Dictionary<string, Parser<string>> _parserFactory = new Dictionary<string, Parser<string>>
    {
        { ColumnConstants.PriceMultiplier, GenerateKeyValueParser(ColumnConstants.PriceMultiplier) }
    };
    public static string GetKeyValueFromString(string fullString, string key)
    {
        if (!_parserFactory.ContainsKey(key))
            return string.Empty;
        var result = _parserFactory[key].TryParse(fullString);
        return result.WasSuccessful ? result.Value : string.Empty;
    }
}