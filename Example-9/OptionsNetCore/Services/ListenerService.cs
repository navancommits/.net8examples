using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionsNetCore.Core.Options.Voter;
using System.Threading;
using System.Threading.Tasks;

namespace OptionsNetCore.Services
{
    public class ListenerService : BackgroundService
    {
        private readonly ILogger<ListenerService> _logger;
        private readonly IOptionsMonitor<VoterOptions> _monitor;

        public ListenerService(IOptionsMonitor<VoterOptions> optionsMonitor, ILogger<ListenerService> logger)
        {
            this._monitor = optionsMonitor;
            this._logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //this._monitor.OnChange(options =>
            //{
            //    var value = Newtonsoft.Json.JsonConvert.SerializeObject(options, Newtonsoft.Json.Formatting.Indented);

            //    this._logger.LogInformation("Change Config");

            //    this._logger.LogInformation(value);
            //});

            return Task.CompletedTask;
        }
    }
}
