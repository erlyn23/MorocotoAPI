﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morocoto.API.Models;
using Morocoto.Infraestructure.Dtos.Requests;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Services.Contracts;
using Morocoto.Infraestructure.Tools.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morocoto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountTools _accountTools;

        public AccountController(IAccountService accountService, IAccountTools accountTools)
        {
            _accountService = accountService;
            _accountTools = accountTools;
        }

        [HttpPost(Name = "SaveUser")]
        public async Task<ActionResult<int>> SaveUserAsync([FromBody] UserRequest userRequest)
        {
            try
            {
                var savedResult = await _accountService.RegisterUserAsync(userRequest);

                if (savedResult > 0)
                    return CreatedAtRoute("SaveUser", new { id = userRequest.Id }, userRequest);
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<UserResponse>> SignInAsync([FromBody] AuthModel authModel)
        {
            try
            {
                var userResponse = await _accountService.SignInAsync(authModel.IdentificationDocument, authModel.UserPassword);

                if (userResponse != null)
                    return Ok(userResponse);
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("SendEmailVerification")]
        public async Task<ActionResult<EmailVerificationResponse>> SendEmailVerificationAsync([FromBody] string userEmail)
        {
            try
            {
                var emailVerificationResponse = await _accountTools.SendEmailConfirmationAsync(userEmail);

                if (emailVerificationResponse != null)
                    return Ok(emailVerificationResponse);
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("VerifyAccount")]
        public async Task<ActionResult<ActivateAccountModel>> ActivateAccountAsync([FromBody] ActivateAccountModel activateAccountModel)
        {
            try
            {
                var activatedAccount = await _accountService.SetAccountActive(activateAccountModel.IdentificationDocument, activateAccountModel.VerificationNumber);

                if (activatedAccount)
                    return Ok(activateAccountModel);
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut("ChangePassword")]
        public async Task<ActionResult<int>> RecoverPasswordAsync([FromBody] ChangePasswordRequest changePasswordRequest) 
        {
            try
            {
                var changePasswordResult = await _accountService.RecoverPasswordAsync(changePasswordRequest);
                if (changePasswordResult > 0)
                    return Ok(changePasswordResult);
                return BadRequest();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
