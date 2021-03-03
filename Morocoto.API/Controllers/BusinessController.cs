using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morocoto.API.Models;
using Morocoto.Domain.Contracts;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Implementations;
using Morocoto.Infraestructure.Services.Contracts;
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
        private readonly IAsyncUnitOfWork _work;
        private readonly IBusinessService _businessService;

        public BusinessController(IAsyncUnitOfWork work, IBusinessService businessService)
        {
            _work = work;
            _businessService = businessService;
        }

        [HttpGet("GetAllBusiness/{partnerId}")]
        public async Task<ActionResult<IEnumerable<BusinessResponse>>> GetAll(string partnerAccountNumber)
        {

            var response = await _work.BusinessRepository.GetAllPartnerBusinessesAsync(x=>x.Partner.IdNavigation.AccountNumber==partnerAccountNumber);


            return Ok(response);
        }

        [HttpPost(Name = "SaveBusiness")]
        public async Task<ActionResult<int>> SaveBusinessAsync(BusinessRequest businessRequest)
        {
            try
            {
                var saveBusinessResult = await _businessService.SaveBusinessAsync(businessRequest);
                if (saveBusinessResult > 0)
                    return CreatedAtRoute("SaveBusiness", new { id = businessRequest.Id }, businessRequest);
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            { 
                await _work.DisposeAsync();
            }
        }
        
        [HttpPost("SellCredit")]
        public async Task<ActionResult<string>> SellCredit([FromBody] SellCreditModel model )
        {
            try
            {
                //Optimization: move this to other service.
                //TODO: hacer catch de errores para lanzar excepciones.
                var business = await _work.BusinessRepository.GetBusinessByAccountNumberAsync(model.BusinessAccountNumber);
                var isAbleToSell = await _work.BusinessRepository.IsAbleForSell(model.BusinessAccountNumber, model.CreditSelled);

                if (isAbleToSell)
                {
                    string response = await _work.BuyCreditRepository.SellCredit(model.BusinessAccountNumber, model.CustomerAccountNumber, (int)model.CreditSelled, model.Pin);
                    await _work.CompleteAsync();
                    return Ok(response);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                await _work.DisposeAsync();
            }
        }
    }
}
