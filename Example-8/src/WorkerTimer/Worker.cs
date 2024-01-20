using System.Text.Json;
using WorkerTimer.Models;

namespace WorkerTimer;
//https://github.com/renatogroffe/DotNet8-WorkerService-IHostedLifecycleService
public class Worker : IHostedLifecycleService
{
    private readonly ILogger<Worker> _logger;
    private readonly ApplicationState _applicationState;
    private Timer? _timer;
    private int Counter { get; set; }


    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _logger.LogInformation("***** IHostedLifecycleService *****");
        _applicationState = new ApplicationState();
        DisplayCurrentTime(_applicationState);
        LogApplicationStatus("Constructor");
    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _applicationState.StartingAsync = true;
        Counter = 100;
        LogApplicationStatus(nameof(StartingAsync));
        
        return Task.CompletedTask;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        _applicationState.StartAsync = true;
        Counter = 200;
        LogApplicationStatus(nameof(StartAsync));
        _logger.LogWarning("Press Ctrl+C to stop worker...");        
        
        return Task.CompletedTask;
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _applicationState.StartedAsync = true;
        for (int i = Counter; i <= 100000; i++)
        { Console.WriteLine(i); }
        Counter = 100000;

        LogApplicationStatus(nameof(StartedAsync));
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Counter countdown stopping...");
        _applicationState.StoppingAsync = true;
        Counter = 2;
        LogApplicationStatus(nameof(StoppingAsync));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _applicationState.StopAsync = true;
        Counter = 1;
        LogApplicationStatus(nameof(StopAsync));
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _applicationState.StoppedAsync = true;
        Counter = 0;

        LogApplicationStatus(nameof(StoppedAsync));
        return Task.CompletedTask;
    }

    private void DisplayCurrentTime(object? state) => _logger.LogInformation(
        $"Actual time: {DateTime.Now:HH:mm:ss} | " +
        $"Worker status: {JsonSerializer.Serialize(state)} | Counter:{Counter}");

    private void LogApplicationStatus(string methodName) => _logger.LogInformation(
        $"{methodName}| Counter:{Counter} | Worker status: {JsonSerializer.Serialize(_applicationState)}");
}