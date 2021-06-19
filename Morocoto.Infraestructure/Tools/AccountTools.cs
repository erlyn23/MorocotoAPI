using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Morocoto.Domain.Contracts;
using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Services.Contracts;
using Morocoto.Infraestructure.Tools.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Tools
{
    public class AccountTools : IAccountTools
    {
        private readonly IConfiguration _configuration;
        private readonly string filePath = $"{Environment.CurrentDirectory}/confirmation_account_data.json";
        private readonly IEmailTools _emailTools;
        private readonly IAsyncUserRepository _userRepository;

        public AccountTools(IConfiguration configuration, IEmailTools emailTools, IAsyncUserRepository userRepository)
        {
            _configuration = configuration;
            _emailTools = emailTools;
            _userRepository = userRepository;
        }

        public async Task<EmailVerificationResponse> SendEmailConfirmationAsync(string userEmail)
        {
            var emailVerification = new EmailVerificationResponse();
            emailVerification.UserEmail = userEmail;
            emailVerification.RandomCode = BuildConfirmations.BuildConfirmationCode();
            emailVerification.ExpireDate = DateTime.UtcNow.AddMinutes(30);
            
            string subject = "Confirmación de cuenta MorocotoApp";
            string body = $"Hola, el número de verificación de tu cuenta de Morocoto es: " +
            $"<b>{emailVerification.RandomCode}</b>, y expira en los próximos <b>30 minutos.</b>";
            try
            {
                var isEmailSended = await _emailTools.SendEmailWithInfoAsync(userEmail, subject, body);
                if (isEmailSended)
                    CreateJsonFileWithConfirmationData(emailVerification);
                return emailVerification;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateJsonFileWithConfirmationData(EmailVerificationResponse emailVerificationResponse)
        {
            var emailVerifications = GetConfirmationDataFromJsonFile();
            EmailVerificationResponse emailVerification = null;

            if (emailVerifications != null)
            {
                emailVerification = emailVerifications
                .Where(e => e.UserEmail == emailVerificationResponse.UserEmail)
                .FirstOrDefault();
            }
            else
            {
                emailVerifications = new List<EmailVerificationResponse>();
            }

            if (emailVerification != null)
            {
                int toDelete = emailVerifications.IndexOf(emailVerification);
                emailVerifications.RemoveAt(toDelete);

                emailVerification.UserEmail = emailVerificationResponse.UserEmail;
                emailVerification.RandomCode = emailVerificationResponse.RandomCode;
                emailVerification.ExpireDate = emailVerificationResponse.ExpireDate;
                emailVerifications.Add(emailVerification);
            }
            else
            {
                emailVerifications.Add(emailVerificationResponse);
            }
            string jsonString = JsonConvert.SerializeObject(emailVerifications);
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, jsonString);
            }
            else
            {
                File.Create(filePath);
                File.WriteAllText(filePath, jsonString);
            }
        }
        public List<EmailVerificationResponse> GetConfirmationDataFromJsonFile()
        {
            string jsonContent = string.Empty;
            using (var streamReader = new StreamReader(filePath)) jsonContent = streamReader.ReadToEnd();
            var emailVerifications = JsonConvert.DeserializeObject<List<EmailVerificationResponse>>(jsonContent);
            return emailVerifications;
        }

        public string BuildToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_configuration["MySecretKey"]);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdentificationDocument),
                new Claim("AccountNumber", user.AccountNumber),
                new Claim("Id", user.Id.ToString()),
                new Claim("UserType", user.UserTypeId.ToString())
            };

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
