using System.ComponentModel.DataAnnotations;

namespace Application.DTO.PaymentDtos
{
    public class UpdatePaymentDto : BasePaymentDto
    {
        [Required]
        public Guid ID { get; set; }
         
    }
}
