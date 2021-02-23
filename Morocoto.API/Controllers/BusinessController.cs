﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morocoto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BusinessController : ControllerBase
    {
        private readonly UnitOfWork _work;

        public BusinessController(UnitOfWork work)
        {
            this._work = work;
        }

        [HttpGet("/getAllBusiness/{partnerId}")]
        public async Task<ActionResult<IEnumerable<BusinessResponse>>> GetAll(int partnerId)
        {
            var response = await _work.BusinessRepository.GetAllPartnerBusinessesAsync(x=>x.PartnerId==partnerId);

            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> SellCredit(int businessAccountNumber,string customerAccountNumber, int creditRequested, string pin )
        {
            var customer = await _work.CustomerRepository.FirstOrDefaultAsync(x=>x.IdNavigation.AccountNumber.Equals(customerAccountNumber));
            var business = await _work.BusinessRepository.GetBusinessByAccountNumberAsync(businessAccountNumber);
            var isAbleToSell = await _work.BusinessRepository.IsAbleForSell(business.BusinessNumber,creditRequested);
            
            if (isAbleToSell)
            {
                string response=await _work.BuyCreditRepository.SellCredit(business,customer,creditRequested,pin);
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
