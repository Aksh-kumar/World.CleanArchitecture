using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using World.Application.Country.Command;
using World.Application.Country.Query;

namespace World.Presentation.Controllers.v2
{
    [ApiVersion(2.0)]
    public class CountryController : APIControllerV2Base
    {
        private readonly ISender _sender;

        public CountryController(ISender sender)
        {
            this._sender = sender;
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> Add(CreateCountryCommand addCountryCommand, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(addCountryCommand, cancellationToken);
            return (result.IsSuccess) ? Ok(result) : StatusCode((int)HttpStatusCode.InternalServerError, result.Error);
        }

        [HttpGet, Route("get")]
        public async Task<IActionResult> Get([FromQuery]string name, CancellationToken cancellationToken)
        {

            GetCountryByNameQuery getCountryByName = new(name);
            var result = await _sender.Send(getCountryByName, cancellationToken);
            return (result.IsSuccess) ? Ok(result) : StatusCode((int)HttpStatusCode.NotFound, result.Error);
        }
    }
}
