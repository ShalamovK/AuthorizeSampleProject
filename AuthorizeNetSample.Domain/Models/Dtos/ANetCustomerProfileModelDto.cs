using System.Collections.Generic;

namespace AuthorizeNetSample.Domain.Models.Dtos {
    public class ANetCustomerProfileModelDto {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public List<PaymentProfileModelDto> PaymentProfiles { get; set; }
        public List<ANetAddressModelDto> Shippings { get; set; }

        public ANetCustomerProfileModelDto() {
            PaymentProfiles = new List<PaymentProfileModelDto>();
            Shippings = new List<ANetAddressModelDto>();
        }
    }

    public class ANetAddressModelDto {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
    }

    public class PaymentProfileModelDto {
        public string Id { get; set; }
        public bool Default { get; set; }
        public ANetAddressModelDto BillTo { get; set; }
        public ANetCardModelDto CreditCard { get; set; }
    }

    public class ANetCardModelDto {
        public string Number { get; set; }
        public string ExpirationDate { get; set; }
    }
}
