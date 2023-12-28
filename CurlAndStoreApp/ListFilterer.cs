using CurlAndStore.Models;

public interface IListFilterer
{
    List<Article> filter(List<Article> fromApi, List<string> previousUuids);
}
public class ListFilterer : IListFilterer
{
    public List<Article> filter(List<Article> fromApi, List<string> previousUuids)
    {
        List<Article> freshArticles = new List<Article>();
        freshArticles.AddRange(fromApi);
        foreach (var article in fromApi)
        {
            var match = previousUuids.Where(prev => prev == article.Uuid);
            if (match.Any())
            {
                freshArticles.Remove(article);
            }
        }
        return freshArticles;
    }
}

