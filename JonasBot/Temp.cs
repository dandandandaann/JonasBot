namespace JonasBot;

public class Temp
{

    static async Task<string> GetCatFact()
    {
        throw new NotImplementedException();
        HttpClient client = null;
        var meowFactUri = new Uri("https://meowfacts.herokuapp.com/");

        HttpResponseMessage response = await client.GetAsync(meowFactUri);
        response.EnsureSuccessStatusCode();

        // JsonConvert.DeserializeObject<CatFact>(response.);

        // return URI of the created resource.
        var responseContent = await response.Content.ReadAsAsync<CatFact>();
        return responseContent.data.First();
    }
}
record CatFact
{
    public string[]? data;

    // static async Task<Uri> CreateProductAsync(Product product)
    // {
    //     HttpResponseMessage response = await client.PostAsJsonAsync(
    //         "api/products", product);
    //     response.EnsureSuccessStatusCode();

    //     // return URI of the created resource.
    //     return response.Headers.Location;
    // }

    // static async Task<Product> GetProductAsync(string path)
    // {
    //     Product product = null;
    //     HttpResponseMessage response = await client.GetAsync(path);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         product = await response.Content.ReadAsAsync<Product>();
    //     }
    //     return product;
    // }
}
