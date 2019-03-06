using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoresample.Customers.Queries.GetCompanyName
{
    public class GetCompanyNameQuery : IRequest<string>
    {
        public string Id { get; set; }
    }
}
