using System.Text.RegularExpressions;

namespace CountEat.API.Helpers;

public static class IngredientParser
{
    // Sık karşılaşılan birimleri listeleyelim
    private static readonly string[] KnownUnits = new[]
{
    "yemek kaşığı", "tatlı kaşığı", "çay kaşığı", "çorba kaşığı", "su bardağı", "çay bardağı", "kahve fincanı",
    "kibrit kutusu", "fincan", "kupa", "bardak", "kase", "dilim", "avuç", "paket", "parça", "demet", "tutam", "çimdik",
    "gram", "gr", "kg", "ml", "litre", "adet", "diş"
};

    public static (double? Quantity, string? Unit, string? Name) ParseIngredientString(string input)
    {
        // baştaki ● ve boşlukları sil
        input = input.TrimStart('●', ' ', '\t');

        // sayı + birim + isim yakala
        var pattern = @"^(?<quantity>(\d+\s+\d+/\d+)|(\d+/\d+)|(\d+([.,]\d+)?(-\d+([.,]\d+)?)?))\s*(?<unit>[^\d]+?)?\s+(?<name>.+)$";
        var match = Regex.Match(input, pattern);

        if (!match.Success)
            return (null, null, input); // fallback: sadece name

        double? quantity = null;
        var quantityRaw = match.Groups["quantity"].Value.Replace(',', '.').Trim();

        if (quantityRaw.Contains('-')) // range
        {
            var firstPart = quantityRaw.Split('-')[0].Trim();
            if (double.TryParse(firstPart, out var parsedQty))
                quantity = parsedQty;
        }
        else if (quantityRaw.Contains('/')) // fraction
        {
            var parts = quantityRaw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                var whole = double.Parse(parts[0]);
                var fracParts = parts[1].Split('/');
                var fraction = double.Parse(fracParts[0]) / double.Parse(fracParts[1]);
                quantity = whole + fraction;
            }
            else if (parts.Length == 1)
            {
                var fracParts = parts[0].Split('/');
                quantity = double.Parse(fracParts[0]) / double.Parse(fracParts[1]);
            }
        }
        else // plain number
        {
            if (double.TryParse(quantityRaw, out var parsedQty))
                quantity = parsedQty;
        }

        var unitRaw = match.Groups["unit"].Value?.Trim().ToLower();

        // Unit bizim bildiğimiz birimlerden biriyse al
        var unit = KnownUnits.FirstOrDefault(u =>
            unitRaw != null && (unitRaw.Equals(u) || unitRaw.Contains(u))
        );

        var name = match.Groups["name"].Value?.Trim();
        if (name == null)
            throw new InvalidOperationException("Name group is missing in the match.");

        var matchedUnit = KnownUnits
            .OrderByDescending(u => u.Length) // önce en uzun unit'i dener
            .FirstOrDefault(u => name.StartsWith(u, StringComparison.OrdinalIgnoreCase));


        if (!string.IsNullOrEmpty(matchedUnit))
        {
            unit = matchedUnit;
            // 1. Tüm unit stringini komple sil (örn: "çay kaşığı")
            name = Regex.Replace(name, $@"\b{Regex.Escape(unit)}\b", "", RegexOptions.IgnoreCase).Trim();

            // 2. Ardından parçalayarak her bir kelimeyi sil (örn: "çay", "kaşığı")
            foreach (var word in unit.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                name = Regex.Replace(name, $@"\b{Regex.Escape(word)}\b", "", RegexOptions.IgnoreCase).Trim();
            }

            // 3. Ekstra önlemler (köke göre)
            name = Regex.Replace(name, @"\bkaşık\w*\b", "", RegexOptions.IgnoreCase).Trim();
            name = Regex.Replace(name, @"\bkaşığ\w*\b", "", RegexOptions.IgnoreCase).Trim();
            name = Regex.Replace(name, @"\bbardak\w*\b", "", RegexOptions.IgnoreCase).Trim();
            name = Regex.Replace(name, @"\bbardağ\w*\b", "", RegexOptions.IgnoreCase).Trim();
        }

        // 🔥 Name’in başında hâlâ “kaşığı un” gibi kalmışsa temizle:
        // 🔥 Name’in başında hâlâ “kaşığı un” gibi kalmışsa temizle:
        name = Regex.Replace(name, @"^\s*(kaşık\w*|kaşığ\w*|bardak\w*|bardağ\w*)\s+", "", RegexOptions.IgnoreCase).Trim();

        // 🔧 Yeni ek: içerde kalan full unit isimlerini sil
        name = Regex.Replace(name, @"\b(?:tatlı\s*kaşığı|yemek\s*kaşığı|çay\s*kaşığı|çorba\s*kaşığı|su\s*bardağı|çay\s*bardağı|kahve\s*fincanı)\b", "", RegexOptions.IgnoreCase).Trim();
        name = Regex.Replace(name, @"\b(?:kaşık\w*|kaşığ\w*|bardak\w*|bardağ\w*|fincan\w*|fincağ\w*)\b", "", RegexOptions.IgnoreCase).Trim();

        name = CleanName(name);

        return (quantity, unit, name);
    }

    public static string CleanName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return name;

        var descriptors = new[]
        {
        "büyük boy", "küçük boy", "orta boy", "taze", "iri", "doğranmış", "kıyılmış", "rendelenmiş", "haşlanmış",
        "ince doğranmış", "bütün", "kabuklu", "kabuğu soyulmuş", "dilimlenmiş", "çekirdeksiz", "küp doğranmış" , "kırmızı toz", "sivri", "tepeleme", "yumurt sarısı", "yumurta sarısı",
        "köftelik", "pilavlık", "ayak", "küçük", "büyük", "orta"
    };

        foreach (var d in descriptors)
        {
            name = Regex.Replace(name, $@"\b{Regex.Escape(d)}\b", "", RegexOptions.IgnoreCase);
        }

        return name.Trim();
    }

    public static string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return name;

        name = name.ToLowerInvariant().Trim();

        // Noktalama ve ekstra boşlukları temizle
        name = Regex.Replace(name, @"[^\w\s]", ""); // noktalama işaretlerini çıkar
        name = Regex.Replace(name, @"\s+", " "); // fazla boşlukları azalt

        // Sık karşılaşılan kelime varyasyonlarını sadeleştir
        name = Regex.Replace(name, @"\b(orta yağlı|az yağlı|yağlı|orta boy|büyük boy|küçük boy)\b", "", RegexOptions.IgnoreCase);
        name = Regex.Replace(name, @"\b(kıyılmış|doğranmış|rendelenmiş|haşlanmış|kavrulmuş|ince doğranmış|dilimlenmiş|çekirdeksiz)\b", "", RegexOptions.IgnoreCase);

        return name.Trim();
    }

}

