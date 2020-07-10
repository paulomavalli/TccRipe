using RIPE.Domain.Domains.PriorityAggregate;
using Xunit;

namespace RIPE.Tests.Domain
{
    public class ReportServiceTests
    {
        private readonly IReportService _collateralPriorityService;
        public ReportServiceTests()
        {
            _collateralPriorityService = new ReportService();
        }

        [Fact]
        public void ShouldReturnCorrectUnitQuantity_FundsProduct()
        {

        }

    }
}
