using Azure.Security.KeyVault.Secrets;
using CurlAndStore.Models;
using CurlAndStoreApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public interface IDbCrud
{
    Task<bool> Store(List<Article> articleList);
    Task<List<string>> Read(string category);
}
public class DbCrud : IDbCrud
{

    private Azure.Response<KeyVaultSecret> _config;

    public DbCrud(Azure.Response<KeyVaultSecret> dbString)
    {
        _config = dbString;
    }

    public async Task<List<string>> Read(string category)
    {
        try
        {
            var context = new DataContextEF(_config);
            return await context.Articles.Select(q => q.Uuid).ToListAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine("Problem reading from the db", ex.Message);
            return new List<string>();

        }

    }
    public async Task<bool> Store(List<Article> articleList)
    {
        
        try
        {
            var context = new DataContextEF(_config);
            await context.Articles.AddRangeAsync(articleList);
            await context.SaveChangesAsync();
            return true;


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Problem storing article data to Db {ex}");
            return false;
        }


    }
}

