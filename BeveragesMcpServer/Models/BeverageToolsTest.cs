using System;

namespace BeveragesMcpServer.Models;

public class BeverageToolsTest
{
    public static void RunTests()
    {
        Console.WriteLine("Starting BeverageTools Tests...\n");

        // Test GetBeveragesJson
        Console.WriteLine("Testing GetBeveragesJson:");
        var beverages = BeverageTools.GetBeveragesJson();
        Console.WriteLine($"Got beverages list: {beverages}\n");

        // Test GetBeverageCount
        Console.WriteLine("Testing GetBeverageCount:");
        var count = BeverageTools.GetBeverageCount();
        Console.WriteLine($"Total beverages: {count}\n");

        // Test GetBeverageJson with a name
        Console.WriteLine("Testing GetBeverageJson by name:");
        var beverageByName = BeverageTools.GetBeverageJson("Coffee");
        Console.WriteLine($"Got beverage by name: {beverageByName}\n");

        // Test GetBeverageByIdJson
        Console.WriteLine("Testing GetBeverageByIdJson:");
        var beverageById = BeverageTools.GetBeverageByIdJson(1);
        Console.WriteLine($"Got beverage by ID: {beverageById}\n");

        // Test GetBeveragesByTypeJson
        Console.WriteLine("Testing GetBeveragesByTypeJson:");
        var beveragesByType = BeverageTools.GetBeveragesByTypeJson("Hot");
        Console.WriteLine($"Got beverages by type: {beveragesByType}\n");

        // Test GetBeveragesByFirstNameJson
        Console.WriteLine("Testing GetBeveragesByFirstNameJson:");
        var beveragesByFirstName = BeverageTools.GetBeveragesByFirstNameJson("Iced");
        Console.WriteLine($"Got beverages by first name: {beveragesByFirstName}\n");

        // Test GetBeveragesByLastNameJson
        Console.WriteLine("Testing GetBeveragesByLastNameJson:");
        var beveragesByLastName = BeverageTools.GetBeveragesByLastNameJson("Tea");
        Console.WriteLine($"Got beverages by last name: {beveragesByLastName}\n");

        Console.WriteLine("All tests completed!");
    }
}