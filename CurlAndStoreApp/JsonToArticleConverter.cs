using System.Text.Json;
using CurlAndStore.Models;

public interface IJsonToArticleConverter
{
    List<Article> Convert(string json);
}

public class JsonToArticleConverter : IJsonToArticleConverter
{
    public List<Article> Convert(string json)
    {
        List<Article> articleList = new List<Article>();
        var root = JsonSerializer.Deserialize<Root>(json);

        var generalList = JsonArticleToArticle(root.data.general);
        var politicsList = JsonArticleToArticle(root.data.politics);
        var businessList = JsonArticleToArticle(root.data.business);
        var techList = JsonArticleToArticle(root.data.tech);
        var sportsList = JsonArticleToArticle(root.data.sports);
        var entertainmentList = JsonArticleToArticle(root.data.entertainment);

        articleList.AddRange(generalList);
        articleList.AddRange(politicsList);
        articleList.AddRange(businessList);
        articleList.AddRange(techList);
        articleList.AddRange(sportsList);
        articleList.AddRange(entertainmentList);

        List<Article> uniqueList = new List<Article>();

        foreach (var article in articleList)
        {
            var match = uniqueList.Where(a => a.Uuid == article.Uuid);
            if (!match.Any())
            {
                uniqueList.Add(article);
            }
        }

        return uniqueList;



    }

    public List<Article> JsonArticleToArticle(IReadOnlyList<JsonArticle> someList) 
    {
        List<Article> articleList = new List<Article>();
        foreach (var article in someList)
        {
            string category1 = article.categories[0];
            string category2;
            if (article.categories.Count > 1)
            {

                category2 = article.categories[1];
            }
            else
            {
                category2 = "null";
            }

            Article oneArticle = new Article(article.uuid,
                article.title, article.description,
                article.keywords, article.snippet, article.url,
                article.image_url, article.language,
               article.published_at, article.source, category1, category2,
                 article.locale);



            articleList.Add(oneArticle);
        }
        return articleList;

    }
    
}

