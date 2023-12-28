public interface IApiDataReader
{
    Task<string> Read(string baseAddress, string requestUri, string category);
}

public class ApiDataReader : IApiDataReader
{
    public async Task<string> Read(string baseAddress, string requestUri, string cateogry)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(baseAddress);
        try
        {
            HttpResponseMessage response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex) 
        {
            Console.Write("APi Reader failed",ex.ToString());
            throw;
        }
        
    }
}

