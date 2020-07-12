namespace RIPE.Domain.Domains
{
    public class Collateral
    {
        public Collateral(string collateralId, string consumerId, int consumerTypeId, string securityId,
                          string securityType, decimal quantity)
        {
            CollateralId = collateralId;
            ConsumerId = consumerId;
            ConsumerTypeId = consumerTypeId;
            SecurityId = securityId;
            SecurityType = securityType;
            Quantity = quantity;

        }

        public string CollateralId { get; set; }
        public string ConsumerId { get; set; }
        public int ConsumerTypeId { get; set; }
        public string SecurityId { get; set; }
        public string SecurityType { get; set; }
        public decimal Quantity { get; set; }

    }


}
