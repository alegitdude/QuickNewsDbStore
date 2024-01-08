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




SecretClientOptions options = new SecretClientOptions()
{
    Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 3,
            
         }
};

var client = new SecretClient(new Uri("https://NewsVault.vault.azure.net/"), new DefaultAzureCredential(), options);

//KeyVaultSecret apiSecret = await client.GetSecretAsync("ApiToken");
//KeyVaultSecret dbSecret = await client.GetSecretAsync("SqlDbPass");

//string apiKey = apiSecret.Value;
//string dbString = dbSecret.Value;

string apiKey = "zRTgVLyhrhkzAR5BYD5pI2b6tmesa0ZVuo7Mh9sZ";
string dbString = "Server=tcp:news-server.database.windows.net,1433;Initial Catalog=NewsDb;Persist Security Info=False;User ID=newsapp;Password=Trfodj51;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

IApiDataReader apiDataReader = new ApiDataReader();
IJsonToArticleConverter jsonConverter = new JsonToArticleConverter();

IDbCrud dbCrud = new DbCrud(dbString);
var baseAddress = "https://api.thenewsapi.com/v1/";
IListFilterer theFilter = new ListFilterer();


var requestUri = $"news/headlines?locale=us&language=en&api_token={apiKey}&headlines_per_category=10&include_similar=false";
   
var json = await apiDataReader.Read(baseAddress, requestUri);
if (!json.Any())
   {
     Console.WriteLine("Didn't read news api");
    return;
    }
    var previousUuids = await dbCrud.Read();
    if (!previousUuids.Any())
    {
        Console.WriteLine("Didn't read previous Uuids");
    return;
    }
    
    var newArticles = jsonConverter.Convert(json);

    var freshArticles = theFilter.filter(newArticles, previousUuids);
if (!freshArticles.Any())
   {
     Console.WriteLine("No fresh articles");
    Console.ReadLine();
     return;
   }

var result = await dbCrud.Store(freshArticles);
Console.WriteLine(result);



public record Business(
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
    [property: JsonPropertyName("categories")] IReadOnlyList<string> categories,
    [property: JsonPropertyName("locale")] string locale,
    [property: JsonPropertyName("similar")] IReadOnlyList<object> similar
);

public record Data(
    [property: JsonPropertyName("general")] IReadOnlyList<JsonArticle> general,
    [property: JsonPropertyName("politics")] IReadOnlyList<JsonArticle> politics,
    [property: JsonPropertyName("business")] IReadOnlyList<JsonArticle> business,
    [property: JsonPropertyName("entertainment")] IReadOnlyList<JsonArticle> entertainment,
    [property: JsonPropertyName("tech")] IReadOnlyList<JsonArticle> tech,
    [property: JsonPropertyName("sports")] IReadOnlyList<JsonArticle> sports
);

public record Entertainment(
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
    [property: JsonPropertyName("categories")] IReadOnlyList<string> categories,
    [property: JsonPropertyName("locale")] string locale,
    [property: JsonPropertyName("similar")] IReadOnlyList<object> similar
);

public record General(
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
    [property: JsonPropertyName("categories")] IReadOnlyList<string> categories,
    [property: JsonPropertyName("locale")] string locale,
    [property: JsonPropertyName("similar")] IReadOnlyList<object> similar
);

public record Politics(
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
    [property: JsonPropertyName("categories")] IReadOnlyList<string> categories,
    [property: JsonPropertyName("locale")] string locale,
    [property: JsonPropertyName("similar")] IReadOnlyList<object> similar
);

public record Root(
    [property: JsonPropertyName("data")] Data data
);

public record Sports(
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
    [property: JsonPropertyName("categories")] IReadOnlyList<string> categories,
    [property: JsonPropertyName("locale")] string locale,
    [property: JsonPropertyName("similar")] IReadOnlyList<object> similar
);

public record Tech(
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
    [property: JsonPropertyName("categories")] IReadOnlyList<string> categories,
    [property: JsonPropertyName("locale")] string locale,
    [property: JsonPropertyName("similar")] IReadOnlyList<object> similar
);


public record JsonArticle(
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
    [property: JsonPropertyName("categories")] IReadOnlyList<string> categories,
    [property: JsonPropertyName("locale")] string locale,
    [property: JsonPropertyName("similar")] IReadOnlyList<object> similar
    );

//public enum JsonArticle
//{ 
//    Tech,
//    Business,
//    Sports,
//    General,
//    Politics,
//}
