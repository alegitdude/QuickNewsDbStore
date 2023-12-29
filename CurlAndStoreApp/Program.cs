using System.Net;
using System.Text.Json.Serialization;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CurlAndStore.Models;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;



List<string> categories = new List<string>
    {
    "general",
    "politics",
    "business",
    "tech",
    "science",
    "sports",
    "health",
    "entertainment",
    "food",
    "travel"
};


var kvUri = "https://newsvault.vault.azure.net/";

var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
var apiKey = await client.GetSecretAsync("ApiToken");
var dbString = await client.GetSecretAsync("SqlDbPass");
Console.WriteLine(apiKey);


IApiDataReader apiDataReader = new ApiDataReader();
IJsonToArticleConverter jsonConverter = new JsonToArticleConverter();

IDbCrud dbCrud = new DbCrud(dbString);
var baseAddress = "https://api.thenewsapi.com/v1/";
IListFilterer theFilter = new ListFilterer();

foreach (string category in categories)
{
    var requestUri = $"news/top?api_token={apiKey}&locale=us&limit=25&categories={category}";
    var json = await apiDataReader.Read(baseAddress, requestUri, category);
    if (!json.Any())
    {
        Console.WriteLine("Didn't read news api");
        continue;
    }
    var previousUuids = await dbCrud.Read(category);
    if (!previousUuids.Any())
    {
        Console.WriteLine("Didn't read previous Uuids");
        continue;
    }
    bool topStory = true;
    var newArticles = jsonConverter.Convert(json, topStory);

    var freshArticles = theFilter.filter(newArticles, previousUuids);
    if (!freshArticles.Any())
    {
        Console.WriteLine("No fresh articles");
        continue;
    }

    var result = await dbCrud.Store(freshArticles);
    Console.WriteLine(result);

}
foreach (string category in categories)
{
    var requestUri = $"news/all?api_token={apiKey}&locale=us&limit=25&categories={category}&language=en";
    var json = await apiDataReader.Read(baseAddress, requestUri, category);
    if (!json.Any())
    {
        Console.WriteLine("Didn't read news api");
        continue;
    }
    var previousUuids = await dbCrud.Read(category);
    if (!previousUuids.Any())
    {
        Console.WriteLine("Didn't read previous Uuids");
        continue;
    }
    bool topStory = false;
    var newArticles = jsonConverter.Convert(json, topStory);

    var freshArticles = theFilter.filter(newArticles, previousUuids);
    if (!freshArticles.Any())
    {
        Console.WriteLine("No fresh articles");
        continue;
    }

    var result = await dbCrud.Store(freshArticles);
    Console.WriteLine(result);

}




public record Datum(
    [property: JsonPropertyName("uuid")] string uuid,
    [property: JsonPropertyName("title")] string title,
    [property: JsonPropertyName("description")] string description,
    [property: JsonPropertyName("keywords")] string keywords,
    [property: JsonPropertyName("snippet")] string snippet,
    [property: JsonPropertyName("url")] string url,
    [property: JsonPropertyName("image_url")] string image_url,
    [property: JsonPropertyName("language")] string language,
    [property: JsonPropertyName("published_at")] DateTime published_at,
    [property: JsonPropertyName("source")] string source,
    [property: JsonPropertyName("categories")] List<string> categories,
    [property: JsonPropertyName("relevance_score")] Nullable<double> relevance_score,
    [property: JsonPropertyName("locale")] string locale
);

public record Meta(
    [property: JsonPropertyName("found")] int found,
    [property: JsonPropertyName("returned")] int returned,
    [property: JsonPropertyName("limit")] int limit,
    [property: JsonPropertyName("page")] int page
);

public record Root(
    [property: JsonPropertyName("meta")] Meta meta,
    [property: JsonPropertyName("data")] IReadOnlyList<Datum> data
);

