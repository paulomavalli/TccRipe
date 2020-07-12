using System;

namespace RIPE.Domain.Domains
{
    public class ProductRequest
    {
        public ProductRequest()
        {

        }

        public ProductRequest(string securityId, string securityType, string securityDescription,
                        decimal quantity, decimal netValue, DateTime? expirationDate)
        {
            SecurityId = securityId;
            SecurityType = securityType;
            SecurityDescription = securityDescription;
            Quantity = quantity;
            NetValue = netValue;
            ExpirationDate = expirationDate;
        }

        public string SecurityId { get; set; }
        public string SecurityType { get; set; }
        public string SecurityDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal NetValue { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}