using System.Collections.Generic;

namespace RIPE.Tests.Fake
{
    public static class CollateralFake
    {

        public static List<RIPE.Domain.Domains.Collateral> GetRealCollateral()
        {
            var collaterals = new List<RIPE.Domain.Domains.Collateral>
            {
                new RIPE.Domain.Domains.Collateral("1", "1", 1, "securityId", "securityType", 0.5M),
                new RIPE.Domain.Domains.Collateral("2", "1", 1, "securityId2", "securityType2", 3)
            };

            return collaterals;
        }

        public static List<RIPE.Domain.Domains.Collateral> GetRealCollateralSameSecurityId()
        {
            var collaterals = new List<RIPE.Domain.Domains.Collateral>
            {
                new RIPE.Domain.Domains.Collateral("1", "1", 1, "1615342", "CDB", 5000.00M)
            };

            return collaterals;
        }

        public static List<RIPE.Domain.Domains.Collateral> GetRealCollateralSameSecurityId(string securityId, decimal collateralQuantity)
        {
            var collaterals = new List<RIPE.Domain.Domains.Collateral>
            {
                new RIPE.Domain.Domains.Collateral("1", "1", 1, securityId, "CDB", collateralQuantity)
            };

            return collaterals;
        }
    }
}
