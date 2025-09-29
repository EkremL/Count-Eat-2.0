namespace CountEat.API.Helpers;

public static class StringHelper
{
    public static string NormalizeString(string input)
    {
        return input
            .ToLower()
            .Replace("ı", "i")
            .Replace("ğ", "g")
            .Replace("ü", "u")
            .Replace("ş", "s")
            .Replace("ö", "o")
            .Replace("ç", "c");
    }
}

