using System;
using BeveragesMcpServer.Models;

namespace BeveragesMcpServer.Tests;

public class BeverageToolsTest
{
    public static void RunTests()
    {
        Console.WriteLine("Starting BeverageTools Tests...\n");

        // Test GetBeveragesJson
        Console.WriteLine("Testing GetBeveragesJson:");
        var beverages = BeverageTools.GetBeveragesJson();
        Console.WriteLine($"Got beverages list: {beverages}\n");

        // Test GetBeveragesByNameJson
        Console.WriteLine("Testing GetBeveragesByNameJson:");
        var beverageByName = BeverageTools.GetBeveragesByNameJson("Coffee");
        Console.WriteLine($"Got beverage by name: {beverageByName}\n");

        // Test GetBeverageByIdJson
        Console.WriteLine("Testing GetBeverageByIdJson:");
        var beverageById = BeverageTools.GetBeverageByIdJson(1);
        Console.WriteLine($"Got beverage by ID: {beverageById}\n");

        // Test GetBeveragesByTypeJson
        Console.WriteLine("Testing GetBeveragesByTypeJson:");
        var beveragesByType = BeverageTools.GetBeveragesByTypeJson("Hot");
        Console.WriteLine($"Got beverages by type: {beveragesByType}\n");

        Console.WriteLine("All tests completed!");
    }
}