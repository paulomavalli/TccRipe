namespace RIPE.Domain.Domains
{
    public class ProductResponse
    {
        public ProductResponse()
        {

        }

        public ProductResponse(string securityId, string securityType, string securityDescription,
                        decimal quantity)
        {
            SecurityId = securityId;
            SecurityType = securityType;
            SecurityDescription = securityDescription;
            Quantity = quantity;
        }

        public string SecurityId { get; set; }
        public string SecurityType { get; set; }
        public string SecurityDescription { get; set; }
        public decimal Quantity { get; set; }
    }
}