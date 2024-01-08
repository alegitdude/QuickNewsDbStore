using Azure.Security.KeyVault.Secrets;
using CurlAndStore.Models;
using CurlAndStoreApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public interface IDbCrud
{
    Task<bool> Store(List<Article> articleList);
    Task<List<string>> Read();
}
public class DbCrud : IDbCrud
{

    private string _config;

    public DbCrud(string dbString)
    {
        _config = dbString;
    }

    public async Task<List<string>> Read()
    {
        try
        {
            var context = new DataContextEF(_config);
            return await context.Articles.Select(q => q.Uuid).AsNoTracking().ToListAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem reading from the db {ex} etc");
            return new List<string>();

        }

    }
    public async Task<bool> Store(List<Article> articleList)
    {

        try
        {
            var newContext = new DataContextEF(_config);
            
            await newContext.Articles.AddRangeAsync(articleList);
            await newContext.SaveChangesAsync();
            Console.WriteLine(articleList.Count(), "Articles");
            return true;


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem storing article data to Db {ex}");
            return false;
        }


    }
}

