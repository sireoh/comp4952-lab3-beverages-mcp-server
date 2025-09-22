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
      string fileName = "../Data/beverages.json";
      string jsonString = File.ReadAllText(fileName);
      var beverages = System.Text.Json.JsonSerializer.Deserialize<List<Beverage>>(jsonString);
      return beverages;
    }
    catch (Exception ex)
    {
      await Console.Error.WriteLineAsync($"Error fetching beverages from JSON: {ex.Message}");
    }
    return [];
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

  public async Task<Beverage?> GetBeverageByName(string name)
  {
    var beverages = await GetBeverages();

    var nameParts = name.Split(' ', 2);
    if (nameParts.Length != 2)
    {
      Console.WriteLine("Name does not contain two parts");
      return null;
    }

    var firstName = nameParts[0].Trim();
    var lastName = nameParts[1].Trim();

    foreach (var s in beverages.Where(s => s.FirstName?.Contains(firstName, StringComparison.OrdinalIgnoreCase) == true))
    {
      Console.WriteLine($"Found partial first name match: '{s.FirstName}' '{s.LastName}'");
    }

    var beverage = beverages.FirstOrDefault(m =>
    {
      var firstNameMatch = string.Equals(m.FirstName, firstName, StringComparison.OrdinalIgnoreCase);
      var lastNameMatch = string.Equals(m.LastName, lastName, StringComparison.OrdinalIgnoreCase);
      return firstNameMatch && lastNameMatch;
    });

    return student;
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

  public async Task<List<Beverage>> GetBeveragesByType(string type)
  {
    var beverages = await GetBeverages();
    var filteredBeverages = beverages.Where(s => s.Type?.Equals(type, StringComparison.OrdinalIgnoreCase) == true).ToList();

    Console.WriteLine(filteredBeverages.Count == 0
        ? $"No beverages found for type: {type}"
        : $"Found {filteredBeverages.Count} beverages for type: {type}");

    return filteredBeverages;
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

  public async Task<List<Beverage>> GetBeveragesByOriginJson(string origin)
  {
    var beverages = await GetBeverages();
    var filteredBeverages = beverages.Where(s => s.Origin?.Equals(origin, StringComparison.OrdinalIgnoreCase) == true).ToList();

    Console.WriteLine(filteredBeverages.Count == 0
        ? $"No beverages found for origin: {origin}"
        : $"Found {filteredBeverages.Count} beverages for origin: {origin}");

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
