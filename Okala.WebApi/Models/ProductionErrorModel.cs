namespace Okala.WebApi.Models;

public record ProductionErrorModel(int StatusCode, string Message);

public record DevelopmentErrorModel(int StatusCode,
    string Message,
    string Detailed,
    string? StackTrace) : ProductionErrorModel(StatusCode,Message);