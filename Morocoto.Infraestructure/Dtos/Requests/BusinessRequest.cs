using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Morocoto.Infraestructure.Dtos.Requests
{
    public class BusinessRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se necesita un socio")]
        public int PartnerId { get; set; }
        [Required(ErrorMessage = "Se necesita un tipo de negocio")]
        public int BusinessTypeId { get; set; }
        [Required(ErrorMessage = "Se necesita que el negocio tenga un número de cuenta")]
        public string BusinessNumber { get; set; }
        [Required( ErrorMessage = "El negocio tiene que estar identificado")]
        public string BusinessName { get; set; }
        public decimal? BusinessCreditAvailable { get; set; }

        [Required(ErrorMessage = "Se necesita que al menos inserte una dirección")]
        public virtual ICollection<BusinessAddressRequest> BusinessAddresses { get; set; }
        [Required(ErrorMessage = "Se necesita que al menos se inserte un teléfono")]
        public virtual ICollection<BusinessPhoneNumberRequest> BusinessPhoneNumbers { get; set; }
    }
}
