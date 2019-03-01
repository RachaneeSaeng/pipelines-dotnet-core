using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnetcoresample.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailModel>
    {
        private readonly DotNetSampleDbContext _context;

        public GetCustomerDetailQueryHandler(DotNetSampleDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDetailModel> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new Exception("Not found");
            }

            return new CustomerDetailModel
            {
                Id = entity.CustomerId,
                Address = entity.Address,
                CompanyName = entity.CompanyName,
                ContactName = entity.ContactName,
                Phone = entity.Phone
            };
        }
    }
}
