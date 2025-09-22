using System;
using System.Net.Http.Json;

namespace BeveragesMcpServer.Models;

public class BeverageService
{
    readonly HttpClient _httpClient = new();
    private List<Beverage>? _beveragesCache = null;
    private DateTime _cacheTime;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10); // adjust as needed

    private async Task<List<Beverage>> FetchBeveragesFromApi()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://gist.githubusercontent.com/medhatelmasry/fab36e3fac4ddafac0f837c920741eae/raw/734f416e93967c02ce36d404916b06da6de5fa77/beverages.json");
            if (response.IsSuccessStatusCode)
            {
                var beveragesFromApi = await response.Content.ReadFromJsonAsync<List<Beverage>>(BeverageContext.Default.ListBeverage);
                return beveragesFromApi ?? [];
            }
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error fetching beverages from API: {ex.Message}");
        }
        return [];
    }

    public async Task<List<Beverage>> GetBeverages()
    {
        if (_beveragesCache == null || DateTime.UtcNow - _cacheTime > _cacheDuration)
        {
            _beveragesCache = await FetchBeveragesFromApi();
            _cacheTime = DateTime.UtcNow;
        }
        return _beveragesCache;
    }

    public async Task<Beverage?> GetBeverageById(int id)
    {
        var beverages = await GetBeverages();
        var beverage = beverages.FirstOrDefault(b => b.BeverageId == id);
        Console.WriteLine(beverage == null ? $"No beverage found with ID {id}" : $"Found beverage: {beverage}");
        return beverage;
    }

    public async Task<List<Beverage>> GetBeveragesByType(string type)
    {
        var beverages = await GetBeverages();
        var filtered = beverages.Where(b => b.Type?.Equals(type, StringComparison.OrdinalIgnoreCase) == true).ToList();
        Console.WriteLine(filtered.Count == 0 ? $"No beverages found for type: {type}" : $"Found {filtered.Count} beverages for type: {type}");
        return filtered;
    }

    public async Task<List<Beverage>> GetBeveragesByOrigin(string origin)
    {
        var beverages = await GetBeverages();
        var filtered = beverages.Where(b => b.Origin?.Equals(origin, StringComparison.OrdinalIgnoreCase) == true).ToList();
        Console.WriteLine(filtered.Count == 0 ? $"No beverages found for origin: {origin}" : $"Found {filtered.Count} beverages for origin: {origin}");
        return filtered;
    }

    public async Task<string> GetBeveragesJson()
    {
        var beverages = await GetBeverages();
        return System.Text.Json.JsonSerializer.Serialize(beverages);
    }
}
