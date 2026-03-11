using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace HomeAppLBO.Services.Quotes
{
    public interface IQuoteService
    {
        Task<QuoteResult> GetRandomQuoteAsync();
    }

    public sealed class QuoteService : IQuoteService
    {
        private readonly HttpClient httpClient;

        public QuoteService()
        {
            httpClient = new HttpClient();
        }

        public async Task<QuoteResult> GetRandomQuoteAsync()
        {
            string today = DateTime.Today.ToString("yyyyMMdd");

            string savedDate = Preferences.Get("quote_date", "");

            if (savedDate == today)
            {
                return new QuoteResult
                {
                    Quote = Preferences.Get("quote_text", ""),
                    Author = Preferences.Get("quote_author", "")
                };
            }

            string json = await httpClient
                .GetStringAsync("https://zenquotes.io/api/random")
                .ConfigureAwait(false);

            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            string quote = "";
            string author = "";

            if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
            {
                JsonElement first = root[0];

                quote = first.GetProperty("q").GetString() ?? "";
                author = first.GetProperty("a").GetString() ?? "";
            }

            string translatedQuote = await TranslateToFrenchAsync(quote);

            if (string.IsNullOrWhiteSpace(translatedQuote))
            {
                translatedQuote = quote;
            }

            // Sauvegarde citation du jour
            Preferences.Set("quote_text", translatedQuote);
            Preferences.Set("quote_author", author);
            Preferences.Set("quote_date", today);

            return new QuoteResult
            {
                Quote = translatedQuote,
                Author = author
            };
        }

        private async Task<string> TranslateToFrenchAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            try
            {
                string encodedText = WebUtility.UrlEncode(text);
                string url =
                    $"https://api.mymemory.translated.net/get?q={encodedText}&langpair=en|fr";

                string json = await httpClient.GetStringAsync(url).ConfigureAwait(false);

                using JsonDocument document = JsonDocument.Parse(json);
                JsonElement root = document.RootElement;

                if (root.TryGetProperty("responseData", out JsonElement responseData) &&
                    responseData.TryGetProperty("translatedText", out JsonElement translatedText))
                {
                    return WebUtility.HtmlDecode(translatedText.GetString() ?? text);
                }

                return text;
            }
            catch
            {
                return text;
            }
        }
    }

    public sealed class QuoteResult
    {
        public string Quote { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}