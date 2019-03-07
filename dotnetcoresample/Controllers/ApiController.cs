using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcoresample.Customers.Queries.GetCustomerDetail;
using dotnetcoresample.Customers.Queries.GetCompanyName;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using dotnetcoresample.Customers.Commands.UpdateCustomer;

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

        // GET api/customers/getcompanyname/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetCompanyName(string id)
        {
            return Ok(await Mediator.Send(new GetCompanyNameQuery { Id = id }));
        }

        // PUT api/customers/5
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update([FromBody]UpdateCustomerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
