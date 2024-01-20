using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionsNetCore.Core.Options.Voter;

namespace OptionsNetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VoterOptions _options;

        public HomeController(IOptions<VoterOptions> options, ILogger<HomeController> logger)
        {
            this._logger = logger;
            this._options = options.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this._options);
        }
    }
}
