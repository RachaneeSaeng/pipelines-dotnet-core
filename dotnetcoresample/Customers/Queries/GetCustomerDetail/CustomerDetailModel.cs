using dotnetcoresample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotnetcoresample.Customers.Queries.GetCustomerDetail
{
    public class CustomerDetailModel
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public int? Age { get; set; }
        public int? Score { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Customer, CustomerDetailModel>> Projection
        {
            get
            {
                return customer => new CustomerDetailModel
                {
                    Id = customer.CustomerId,
                    Address = customer.Address,                    
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,                    
                    Phone = customer.Phone
                };
            }
        }

        public static CustomerDetailModel Create(Customer customer)
        {
            return Projection.Compile().Invoke(customer);
        }
    }
}
