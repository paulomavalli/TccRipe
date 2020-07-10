using RIPE.Domain.Domains;
using System.Collections.Generic;

namespace RIPE.Tests.Fake
{
    public static class CollateralPriorityFake
    {
        public static List<CollateralPriority> GetRealListPriorities()
        {
            return new List<CollateralPriority>
            {
                new CollateralPriority("TD","IPCA",1 ,"Alta"),
                new CollateralPriority("TD","PRE",1,"Alta"),
                new CollateralPriority("TD","SELIC",1,"Alta"),
                new CollateralPriority("FUNDOS","Ações",2,"Media - Alta"),
                new CollateralPriority("CDB","VENCIMENTO",3,"Media")
            };
        }
    }

}
