using System.Text.Json;
using CurlAndStore.Models;

public interface IJsonToArticleConverter
{
    List<Article> Convert(string json, bool topStory);
}

public class JsonToArticleConverter : IJsonToArticleConverter
{
    public List<Article> Convert(string json, bool topStory)
    {
        List<Article> articleList = new List<Article>();
        var root = JsonSerializer.Deserialize<Root>(json);
        int topStoryBit = 1;
       
        if(topStory == false)
        {
            topStoryBit = 0;
        }
       
        foreach (var article in root.data)
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
            string locale;
            if( topStory ==  false) 
            {
                locale = "none";
            }
            else
            {
                locale = "us";
            }
            Article oneArticle = new Article(article.uuid,
                article.title, article.description,
                article.keywords, article.snippet, article.url,
                article.image_url, article.language,
               article.published_at, article.source, category1, category2,
                 article.relevance_score, locale, topStoryBit);



            articleList.Add(oneArticle);
        }
        return articleList.ToList();
    }
}

