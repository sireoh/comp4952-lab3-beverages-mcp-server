using System;

namespace BeveragesMcpServer.Models;

public class Beverage
{
  public int BeverageId { get; set; }
  public string? Name { get; set; }
  public string? Type { get; set; }
  public string? MainIngredient { get; set; }

  public string? Origin { get; set; }

  public int? CaloriesPerServing { get; set; }

  public override string ToString()
  {
    return $"Beverage ID: {BeverageId}, Name: {Name}, Type: {Type}, Main Ingredient: {MainIngredient}, Origin: {Origin}, Calories per Serving: {CaloriesPerServing}";
  }
}

