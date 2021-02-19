using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Morocoto.Infraestructure.Dtos.Requests
{
    public class ChangePasswordRequest
    {
        public string IdentificationDocument { get; set; }
        [Required(ErrorMessage = "Debes seleccionar una pregunta de seguridad")]
        public int SecurityQuestionId { get; set; }
        [Required(ErrorMessage = "Debes enviar la respuesta de seguridad")]
        public string SecurityQuestionAnswer { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(4, ErrorMessage = "La contraseña debe contener al menos 4 números")]
        [MaxLength(8, ErrorMessage = "La contraseña debe contener solo 8 números")]
        public string Password1 { get; set; }
        [Required(ErrorMessage = "La confirmación de contraseña es requerida")]
        [MinLength(4, ErrorMessage = "La confirmación de contraseña debe contener al menos 4 números")]
        [MaxLength(8, ErrorMessage = "La confirmación de contraseña debe contener solo 8 números")]
        public string Password2 { get; set; }
    }
}
