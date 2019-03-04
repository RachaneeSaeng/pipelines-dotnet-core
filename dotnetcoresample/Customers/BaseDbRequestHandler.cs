using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnetcoresample.Customers
{
    public abstract class BaseDbRequestHandler<TRequest, TResponse> 
        : IRequestHandler<TRequest, TResponse>
        where TRequest: IRequest<TResponse>
    {
        protected readonly DotNetSampleDbContext _context;

        public BaseDbRequestHandler(DotNetSampleDbContext context)
        {
            _context = context;
        }
        
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
