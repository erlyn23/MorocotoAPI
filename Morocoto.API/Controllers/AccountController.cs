using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Morocoto.API.Models;
using Morocoto.Domain.Contracts;
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
        private readonly IAsyncUserRepository _userRepository;
        private readonly IAccountService _accountService;
        private readonly IAccountTools _accountTools;

        public AccountController(IAccountService accountService, IAccountTools accountTools, IAsyncUserRepository userRepository)
        {
            _accountService = accountService;
            _accountTools = accountTools;
            _userRepository = userRepository;
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
                if (string.Equals(ex.Message, "ERRU001"))
                    return BadRequest("El usuario con esta cédula ya existe, intente con una nueva.");
                else if (string.Equals(ex.Message, "ERRU002"))
                    return BadRequest("El correo electrónico ya existe, intente con uno nuevo.");
                else if (string.Equals(ex.Message, "ERRU003"))
                    return BadRequest("El teléfono ya existe, intente con uno nuevo");

                return BadRequest("Ha ocurrido un error internto en la base de datos");
            }
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<UserResponse>> SignInAsync([FromBody] AuthModel authModel)
        {
            try
            {
                var userResponse = await _accountService.SignInAsync(authModel.IdentificationDocument, authModel.UserPassword, authModel.UserPhone, authModel.OsPhone);

                if (userResponse != null)
                    return Ok(userResponse);
                return BadRequest();
            }
            catch(Exception ex)
            {
                if (string.Equals(ex.Message, "ERSI001"))
                    return BadRequest("Usuario o contraseña incorrecta, verifique sus datos.");
                else if (string.Equals(ex.Message, "ERSI002"))
                    return BadRequest("El usuario está inactivo.");
                return BadRequest("Ha ocurrido un error al iniciar sesión.");
            }
        }

        [HttpPost("SendEmailVerification")]
        public async Task<ActionResult<EmailVerificationResponse>> SendEmailVerificationAsync([FromBody] SendEmailRequest sendEmailRequest)
        {
            try
            {
                EmailVerificationResponse emailVerificationResponse = null;
                if (string.Equals(sendEmailRequest.VerificationType, "Register"))
                {
                    emailVerificationResponse = await _accountTools.SendEmailConfirmationAsync(sendEmailRequest.UserEmail);
                }
                else
                {
                    var userInDb = await _userRepository.FirstOrDefaultAsync(u => u.IdentificationDocument == sendEmailRequest.IdentificationDocument);

                    if (userInDb != null)
                    {
                        if (userInDb.Active)
                            return BadRequest("Este usuario ya se encuentra en estado activo, por favor inicie sesión.");
                        emailVerificationResponse = await _accountTools.SendEmailConfirmationAsync(userInDb.Email);
                    }
                    else
                        return BadRequest("Usuario no encontrado en la base de datos");
                }

                if (emailVerificationResponse != null)
                    return Ok(emailVerificationResponse);
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest("Ha ocurrido un error interno al enviar el correo de verificación.");
            }
        }

        [HttpPatch("VerifyAccount")]
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
                if (string.Equals(ex.Message, "ERAC001"))
                    return BadRequest("El código de verificación no es válido.");
                else if (string.Equals(ex.Message, "ERAC002"))
                    return BadRequest("El código de verificación ya expiró.");

                return BadRequest("Ha ocurrido un error interno al verificar cuenta.");
            }
        }
        [HttpPatch("ChangePassword")]
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
                if (string.Equals(ex.Message, "ERCP001"))
                    return BadRequest($"El usuario con la cédula {changePasswordRequest.IdentificationDocument} no existe en la base de datos.");
                else if (string.Equals(ex.Message, "ERCP002"))
                    return BadRequest("La respuesta de seguridad es incorrecta.");
                if (string.Equals(ex.Message, "ERCP003"))
                    return BadRequest("Las contraseñas no coinciden.");
                else if (string.Equals(ex.Message, "ERCP004"))
                    return BadRequest("La contraseña no puede ser igual a la anterior.");


                return BadRequest("Ha ocurrido un error al cambiar contraseña.");
            }
        }
    }
}
