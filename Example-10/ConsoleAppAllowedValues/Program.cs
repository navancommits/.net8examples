using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ConsoleAppAllowedValues.Models;

Console.WriteLine("*****.NET 8 | AllowedValuesAttribute *****");

var states = new State[]
{
    new() {StateCode = "VIC", Name = "Victoria"},
    new() {StateCode = "VOC", Name = "Victoria"},
    new() {StateCode = "TAS", Name = "Tasmania"},
    new() {StateCode = "NSW", Name = "New South Wales"},
    new() {StateCode = "NT", Name = "Northern Territory"},
    new() {StateCode = "WA", Name = "Western Aus"},
    new() {StateCode = "SA", Name = "South Australia"}
};

foreach (var state in states)
{
    Console.WriteLine();
    Console.WriteLine(JsonSerializer.Serialize(state));
    var validationResults = new List<ValidationResult>();
    if (!Validator.TryValidateObject(state, new ValidationContext(state),
        validationResults, validateAllProperties: true))
    {
        Console.WriteLine("Invalid state(s)...");
        foreach (var validationResult in validationResults)
            Console.WriteLine($"ErrorMessage = {validationResult.ErrorMessage}");
    }
}