namespace HexiTech.Models.Orders
{
    using System.ComponentModel.DataAnnotations;

    public class OrderFormModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string CompanyName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string City { get; set; }
        
        [Required]
        public string Province { get; set; }

        [Required]
        public string Address { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string AdditionalInformation { get; set; }
    }
}