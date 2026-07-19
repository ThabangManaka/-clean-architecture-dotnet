using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Commands
{
    public record DeleteBasketByUserNameCommand(string userName) : IRequest<Unit>;
}
