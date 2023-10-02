using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using World.Application.Contracts.Services;

namespace World.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CountryController : APIControllerV1Base
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;
        public CountryController(ICountryService countryService, ILogger<CountryController> logger) : base(lists: countryService)
        {
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet, Route("get")]
        public async Task<IActionResult> Get(string name, CancellationToken cancellation)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            return Ok(await _countryService.GetCountryByName(name, cancellation));
        }
    }
}
