using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoresample.Customers.Queries.GetTopName
{
    public class GetTopNameQuery : IRequest<string>
    {
        public string Id { get; set; }
    }
}
