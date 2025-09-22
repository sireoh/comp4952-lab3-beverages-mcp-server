using System;
using System.Net.Http.Json;

namespace BeveragesMcpServer.Models;

public class BeverageService
{
  private List<Beverage>? _beveragesCache = null;
  private DateTime _cacheTime;
  private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10); // adjust as needed

  private async Task<List<Beverage>> FetchBeveragesFromJson()
  {
    try
    {
      string fileName = Path.Combine(AppContext.BaseDirectory, "Data", "beverages.json");
      string jsonString = File.ReadAllText(fileName);
      var beverages = System.Text.Json.JsonSerializer.Deserialize<List<Beverage>>(jsonString);
      return beverages ?? new List<Beverage>();
    }
    catch (Exception ex)
    {
      await Console.Error.WriteLineAsync($"Error fetching beverages from JSON: {ex.Message}");
      return new List<Beverage>();
    }
  }

  public async Task<List<Beverage>> GetBeverages()
  {
    if (_beveragesCache == null || DateTime.UtcNow - _cacheTime > _cacheDuration)
    {
      _beveragesCache = await FetchBeveragesFromJson();
      _cacheTime = DateTime.UtcNow;
    }
    return _beveragesCache;
  }

  public async Task<Beverage?> GetBeverageById(int id)
  {
    var beverages = await GetBeverages();
    var beverage = beverages.FirstOrDefault(s => s.BeverageId == id);

    Console.WriteLine(beverage == null ? $"No beverage found with ID {id}" : $"Found beverage: {beverage}");
    return beverage;
  }

  public async Task<List<Beverage>> GetBeveragesByName(string name)
  {
    var beverages = await GetBeverages();
    var filteredBeverages = beverages.Where(s => s.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true).ToList();

    Console.WriteLine(filteredBeverages.Count == 0
      ? $"No beverages found with name: {name}"
      : $"Found {filteredBeverages.Count} beverages with name: {name}");

    return filteredBeverages;
  }

  public async Task<Beverage?> GetBeveragesByTypeJson(string type)
  {
    var beverages = await GetBeverages();
    var beverage = beverages.FirstOrDefault(s => s.Type?.Equals(type, StringComparison.OrdinalIgnoreCase) == true);

    Console.WriteLine(beverage == null ? $"No beverage found with type: {type}" : $"Found beverage: {beverage}");
    return beverage;
  }

  public async Task<List<Beverage>> GetBeveragesByMainIngredient(string mainIngredient)
  {
    var beverages = await GetBeverages();
    var filteredBeverages = beverages.Where(s => s.MainIngredient?.Equals(mainIngredient, StringComparison.OrdinalIgnoreCase) == true).ToList();

    Console.WriteLine(filteredBeverages.Count == 0
        ? $"No beverages found for main ingredient: {mainIngredient}"
        : $"Found {filteredBeverages.Count} beverages for main ingredient: {mainIngredient}");

    return filteredBeverages;
  }

  public async Task<List<Beverage>> GetBeveragesByOrigin(string origin)
  {
    var beverages = await GetBeverages();
    var filteredBeverages = beverages.Where(s => s.Origin?.Equals(origin, StringComparison.OrdinalIgnoreCase) == true).ToList();

    Console.WriteLine(filteredBeverages.Count == 0
        ? $"No beverages found from origin: {origin}"
        : $"Found {filteredBeverages.Count} beverages from origin: {origin}");

    return filteredBeverages;
  }

  public async Task<List<Beverage>> GetBeveragesByCaloriesPerServing(int caloriesPerServing)
  {
    var beverages = await GetBeverages();
    var filteredBeverages = beverages.Where(s => s.CaloriesPerServing == caloriesPerServing).ToList();

    Console.WriteLine(filteredBeverages.Count == 0
        ? $"No beverages found with calories per serving: {caloriesPerServing}"
        : $"Found {filteredBeverages.Count} beverages with calories per serving: {caloriesPerServing}");

    return filteredBeverages;
  }

  public async Task<string> GetBeveragesJson()
  {
    var beverages = await GetBeverages();
    return System.Text.Json.JsonSerializer.Serialize(beverages);
  }
}
