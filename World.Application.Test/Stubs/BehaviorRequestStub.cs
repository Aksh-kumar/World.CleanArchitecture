using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Shared;

namespace World.Unit.Test.Stubs;

internal class BehaviorRequestStub<TResponse> : IRequest<TResponse> where TResponse : Result
{
}
