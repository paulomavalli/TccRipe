using RIPE.Domain.Domains;
using System;
using System.Collections.Generic;

namespace RIPE.Tests.Fake
{
    public static class ProductFake
    {
        public static List<ProductResponse> GetRealProductsResponseList()
        {

            List<ProductResponse> products = new List<ProductResponse>
            {
                new ProductResponse("productId", "TD","IPCA", (decimal)0.5),
                new ProductResponse("productId", "TD","PRE", (decimal)0.5),
                new ProductResponse("productId", "TD", "SELIC", (decimal)0.5),
                new ProductResponse("productId", "FUNDOS","Ações", (decimal)0.5),
                new ProductResponse("productId", "FUNDOS","Ações", (decimal)0.5),
                new ProductResponse("productId", "CDB", "VENCIMENTO", 3)
            };

            return products;
        }
        public static List<ProductRequest> GetRealProductsRequestList()
        {

            List<ProductRequest> products = new List<ProductRequest>
            {
                new ProductRequest("productId", "TD","IPCA", (decimal)0.5,  1000, new DateTime(2020,07,15)),
                new ProductRequest("productId", "TD","PRE", (decimal)0.5,  1000, new DateTime(2020,07,15)),
                new ProductRequest("productId", "TD", "SELIC", (decimal)0.5, 1000, new DateTime(2020,07,15)),
                new ProductRequest("productId", "FUNDOS","Ações", (decimal)0.5,  500, new DateTime( 2025,04,15)),
                new ProductRequest("productId", "FUNDOS","Ações", (decimal)0.5,  1000, new DateTime( 2021,04,15)),
                new ProductRequest("productId", "CDB", "VENCIMENTO", 3, 2400, new DateTime( 2025,04,15))
            };

            return products;
        }

        public static ProductRequest GetFundsProduct()
        {
            return new ProductRequest("productId", "FUNDOS", "Ações", (decimal)0.5, 1000, new DateTime(2025, 04, 15));
        }
        public static ProductRequest GetTreasuryDirectProduct()
        {
            return new ProductRequest("productId", "TD", "SELIC", (decimal)0.5, 1000, new DateTime(2025, 04, 15));
        }

        public static ProductRequest GetFixedIncomeProduct()
        {
            return new ProductRequest("productId", "CDB", "VENCIMENTO", 3, 2400, new DateTime(2025, 04, 15));
        }

        public static List<ProductRequest> GetNecessaryProducts()
        {
            List<ProductRequest> products = new List<ProductRequest>
            {
                new ProductRequest("productId", "TD", "SELIC", (decimal)0.5, 1000, new DateTime(2025,04,15)),
                new ProductRequest("productId", "FUNDOS","Ações", (decimal)0.5,  1000, new DateTime(2025,04,15)),
                new ProductRequest("productId", "CDB", "VENCIMENTO", 3, 2400, new DateTime(2025,04,15))
            };

            return products;
        }
    }
}
