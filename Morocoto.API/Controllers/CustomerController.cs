using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Implementations;
using Morocoto.Infraestructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Morocoto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            
            this._customerService = customerService;
        }
        [HttpGet("HasAuthorization")]
        public ActionResult<bool> HasAthorization()
        {
            return Ok(true);
        }
        [HttpPost("transaction")]
        public async Task<ActionResult<string>> CreditTransactions(TransactionsRequest model)
        {

            if (model != null)
            {
                return await _customerService.ExecuteTransactionAsync(model);
            }
            return BadRequest();
        }
    }
}
