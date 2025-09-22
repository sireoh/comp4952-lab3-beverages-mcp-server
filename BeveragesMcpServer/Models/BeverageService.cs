using System.Text.Json;

namespace BeveragesMcpServer.Models;

public class BeverageService
{
    private List<Beverage>? _beveragesCache = null;
    private DateTime _cacheTime;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    private async Task<List<Beverage>> FetchBeveragesFromFile()
    {
        try
        {
            // Path is relative to the app base directory
            var path = Path.Combine(AppContext.BaseDirectory, "Data", "beverages.json");

            if (File.Exists(path))
            {
                var json = await File.ReadAllTextAsync(path);

                // Use System.Text.Json with source generator (fast) OR reflection fallback
                return JsonSerializer.Deserialize(json, BeverageContext.Default.ListBeverage) ?? new List<Beverage>();
            }
            else
            {
                await Console.Error.WriteLineAsync($"Beverage file not found at {path}");
            }
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error reading beverages.json: {ex.Message}");
        }

        return new List<Beverage>();
    }

    public async Task<List<Beverage>> GetBeverages()
    {
        if (_beveragesCache == null || DateTime.UtcNow - _cacheTime > _cacheDuration)
        {
            _beveragesCache = await FetchBeveragesFromFile();
            _cacheTime = DateTime.UtcNow;
        }
        return _beveragesCache;
    }

    public async Task<Beverage?> GetBeverageById(int id)
    {
        var beverages = await GetBeverages();
        return beverages.FirstOrDefault(b => b.BeverageId == id);
    }

    public async Task<List<Beverage>> GetBeveragesByType(string type)
    {
        var beverages = await GetBeverages();
        return beverages.Where(b => b.Type?.Equals(type, StringComparison.OrdinalIgnoreCase) == true).ToList();
    }

    public async Task<List<Beverage>> GetBeveragesByOrigin(string origin)
    {
        var beverages = await GetBeverages();
        return beverages.Where(b => b.Origin?.Equals(origin, StringComparison.OrdinalIgnoreCase) == true).ToList();
    }

    public async Task<string> GetBeveragesJson()
    {
        var beverages = await GetBeverages();
        return JsonSerializer.Serialize(beverages, BeverageContext.Default.ListBeverage);
    }
}
