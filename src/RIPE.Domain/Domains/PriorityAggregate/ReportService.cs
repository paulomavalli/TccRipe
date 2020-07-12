using RIPE.CrossCutting.Extensions;
using RIPE.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RIPE.Domain.Domains.PriorityAggregate
{
    public class ReportService : IReportService
    {
        public List<ProductRequest> GetCollateralProducts(IEnumerable<ProductRequest> products, decimal loanValue, IEnumerable<CollateralPriority> priorities)
        {
            List<ProductRequest> collateralProducts = new List<ProductRequest>();

            foreach (var priority in priorities)
            {
                var productsPriority = products.Where(x => x.SecurityType == priority.ProductTypeId
                                                      && x.SecurityDescription == priority.ProductTypeDescription)
                                               .OrderByDescending(x => x.ExpirationDate);

                foreach (var product in productsPriority)
                {
                    if (loanValue <= 0)
                        return collateralProducts;

                    var unitQuantity = GetUnitQuantity(product);
                    var necessaryProduct = GetNecessaryCollateralProduct(product, loanValue, unitQuantity);
                    collateralProducts.Add(necessaryProduct);

                    loanValue -= necessaryProduct.NetValue;
                }
            }

            return collateralProducts;
        }

        public decimal GetUnitQuantity(ProductRequest product)
        {
            if (product.SecurityType == ProductType.TD.GetEnumDescription() || product.SecurityType == ProductType.FUNDOS.GetEnumDescription())
                return 0.01M;
            else
                return 1;
        }

        public ProductRequest GetNecessaryCollateralProduct(ProductRequest product, decimal currentLoanValue, decimal unitQuantity)
        {
            ProductRequest necessaryProduct = new ProductRequest(product.SecurityId, product.SecurityType, product.SecurityDescription, 0, 0, null);

            if (currentLoanValue >= product.NetValue)
            {
                necessaryProduct.NetValue = product.NetValue;
                necessaryProduct.Quantity = product.Quantity;
            }
            else
            {
                decimal productQuantity = product.Quantity / unitQuantity;
                decimal unitValue = product.NetValue / productQuantity;

                decimal collateralQuantity = currentLoanValue / unitValue;
                decimal realCollateralQuantity = Math.Ceiling(collateralQuantity);

                necessaryProduct.NetValue = realCollateralQuantity * unitValue;
                necessaryProduct.Quantity = realCollateralQuantity * unitQuantity;
            }



            return necessaryProduct;

        }

    }
}
