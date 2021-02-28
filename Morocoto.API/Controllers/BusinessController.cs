using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morocoto.API.Models;
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

        [HttpGet("GetAllBusiness/{partnerId}")]
        public async Task<ActionResult<IEnumerable<BusinessResponse>>> GetAll(int partnerId)
        {
            var response = await _work.BusinessRepository.GetAllPartnerBusinessesAsync(x=>x.PartnerId==partnerId);

            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> SellCredit([FromBody] SellCreditModel model )
        {
            //Optimization: move this to other service.
            var business = await _work.BusinessRepository.GetBusinessByAccountNumberAsync(model.BusinessAccountNumber);
            var isAbleToSell = await _work.BusinessRepository.IsAbleForSell(model.BusinessAccountNumber,model.CreditSelled);
            
            if (isAbleToSell)
            {
                string response=await _work.BuyCreditRepository.SellCredit(model.BusinessAccountNumber,model.CustomerAccountNumber,(int)model.CreditSelled, model.Pin);
                await _work.CompleteAsync();
                await _work.DisposeAsync();
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
