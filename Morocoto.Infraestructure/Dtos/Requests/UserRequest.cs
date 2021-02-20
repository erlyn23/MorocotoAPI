using Morocoto.Domain.Contracts;
using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Dtos.Requests
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        [MinLength(10, ErrorMessage = "El nombre debe contener al menos 10 letras")]
        [MaxLength(50, ErrorMessage="El nombre no debe contener más de 50 letras")]
        public string FullName { get; set; }
        public string UserPhone { get; set; }
        public string OsPhone { get; set; }
        [Required(ErrorMessage = "La cédula es requerida")]
        [MinLength(11, ErrorMessage = "La cédula solo debe contener 11 números")]
        [MaxLength(11, ErrorMessage = "La cédula solo debe contener 11 números")]
        public string IdentificationDocument { get; set; }
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [MaxLength(50, ErrorMessage = "El correo no debe contener más de 11 letras")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(4, ErrorMessage = "La contraseña debe contener al menos 4 números")]
        [MaxLength(8, ErrorMessage = "La contraseña no debe contener más de 8 números")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "El PIN es requerido")]
        [MinLength(4, ErrorMessage = "El pin solo debe contener 4 números")]
        [MaxLength(4, ErrorMessage = "El pin solo debe contener 4 números")]
        public string Pin { get; set; }
        [Required(ErrorMessage = "La respuesta de seguridad es requerida")]
        public string SecurityAnswer { get; set; }
        [Required(ErrorMessage = "Debes seleccionar un tipo de usuario")]
        public int UserTypeId { get; set; }
        [Required(ErrorMessage = "Debes seleccionar una pregunta de seguridad")]
        public int SecurityQuestionId { get; set; }

        public ICollection<UserAddressRequest> UserAddresses { get; set; }
        public ICollection<UserPhoneNumberRequest> UserPhoneNumbers { get; set; }
    }
}
