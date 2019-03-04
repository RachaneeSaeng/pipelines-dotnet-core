using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcoresample.Customers.Queries.GetCustomerDetail;
using dotnetcoresample.Customers.Queries.GetTopName;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoresample.Controllers
{
    public class ApiController : BaseController
    {
        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailModel>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetCustomerDetailQuery { Id = id }));
        }

        // GET api/customers/gettopname
        [HttpGet]
        public async Task<ActionResult<CustomerDetailModel>> GetTopName()
        {
            return Ok(await Mediator.Send(new GetTopNameQuery()));
        }
    }
}
