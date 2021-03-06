﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public BusinessController(IAsyncUnitOfWork work, IBusinessService businessService, IMapper mapper)
        {
            _work = work;
            _businessService = businessService;
            _mapper = mapper;
        }

        [HttpGet("GetAllBusiness/{partnerId}")]
        public async Task<ActionResult<IEnumerable<BusinessResponse>>> GetAll(int partnerId)
        {

            var response = await _work.BusinessRepository.GetAllPartnerBusinessesAsync(x=>x.Partner.Id==partnerId);
            var responseDTO = _mapper.Map<IEnumerable<BusinessResponse>>(response);

            return Ok(responseDTO);
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
