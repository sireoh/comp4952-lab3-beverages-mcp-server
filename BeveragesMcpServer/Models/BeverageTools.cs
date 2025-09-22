using System;
using System.ComponentModel;
using ModelContextProtocol.Server;

namespace BeveragesMcpServer.Models;

[McpServerToolType]
public static class BeverageTools
{
    private static readonly BeverageService _beverageService = new BeverageService();

    [McpServerTool, Description("Get a list of beverages and return as JSON array")]
    public static string GetBeveragesJson()
    {
        var task = _beverageService.GetBeveragesJson();
        return task.GetAwaiter().GetResult();
    }

    [McpServerTool, Description("Get a beverage by ID and return as JSON")]
    public static string GetBeverageByIdJson([Description("The ID of the beverage to get details for")] int id)
    {
        var task = _beverageService.GetBeverageById(id);
        var beverage = task.GetAwaiter().GetResult();
        if (beverage == null)
        {
            return "Beverage not found";
        }
        return System.Text.Json.JsonSerializer.Serialize(beverage, BeverageContext.Default.Beverage);
    }

    [McpServerTool, Description("Get beverages by type and return as JSON")]
    public static string GetBeveragesByTypeJson([Description("The type of beverage to filter by")] string type)
    {
        var task = _beverageService.GetBeveragesByType(type);
        var beverages = task.GetAwaiter().GetResult();
        return System.Text.Json.JsonSerializer.Serialize(beverages, BeverageContext.Default.ListBeverage);
    }

    [McpServerTool, Description("Get beverages by origin and return as JSON")]
    public static string GetBeveragesByOriginJson([Description("The origin of the beverage to filter by")] string origin)
    {
        var task = _beverageService.GetBeveragesByOrigin(origin);
        var beverages = task.GetAwaiter().GetResult();
        return System.Text.Json.JsonSerializer.Serialize(beverages, BeverageContext.Default.ListBeverage);
    }

    [McpServerTool, Description("Get count of total beverages")]
    public static int GetBeverageCount()
    {
        var task = _beverageService.GetBeverages();
        var beverages = task.GetAwaiter().GetResult();
        return beverages.Count;
    }
}
