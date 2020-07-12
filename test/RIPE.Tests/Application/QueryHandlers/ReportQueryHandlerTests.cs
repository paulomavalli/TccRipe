using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Queries;
using RIPE.Application.QueryHandlers;
using RIPE.Application.Responses;
using RIPE.Domain;
using RIPE.Domain.Domains.Questions;
using RIPE.Tests.Fake;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RIPE.Tests.Application.QueryHandlers
{
    public class ReportQueryHandlerTests
    {
        private readonly IRipeRepository _collateralRepository;
        private readonly ReportQueryHandler _handler;

        public ReportQueryHandlerTests()
        {
            _collateralRepository = Substitute.For<IRipeRepository>();
            var logger = Substitute.For<ILogger<ReportQueryHandler>>();

            _handler = new ReportQueryHandler(logger,
                _collateralRepository);
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsNull()
        {
            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Returns((IEnumerable<RIPE.Domain.Domains.Collateral>)null);

            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();
            Response<ReportResponse> response = await _handler.Handle(new ReportQuery(checkBoxes), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.NivelMaturidade.Should().Be("0");
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsEmpty()
        {
            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Returns(new List<RIPE.Domain.Domains.Collateral>());

            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();
            Response<ReportResponse> response = await _handler.Handle(new ReportQuery(checkBoxes), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.NivelMaturidade.Should().Be("0");
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsLessThanWithdrawalQuantity()
        {
            const string securityId = "12345";
            //  const decimal securityQuantity = 10000.506M;
            // const decimal withdrawalQuantity = 6000.342M;
            const decimal quantityInCollateral = 5000.231M;

            List<RIPE.Domain.Domains.Collateral> collaterals = CollateralFake.GetRealCollateralSameSecurityId(securityId, quantityInCollateral);
            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Returns(collaterals);

            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();
            Response<ReportResponse> response = await _handler.Handle(new ReportQuery(checkBoxes), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.PorcentagemRespostasPositivas.Should().Be("55");
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_WithEmptyRequest()
        {
            Response<ReportResponse> response = await _handler.Handle((ReportQuery)null, CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            response.Messages.Contains(Messages.InvalidRequest).Should().BeTrue();
        }

        [Fact]
        //[InlineData(Messages.InvalidCustomerId, "", "12345", 1000, 500)]
        //[InlineData(Messages.InvalidSecurityId, "123456789", "", 1000, 500)]
        //[InlineData(Messages.InvalidSecurityQuantity, "123456789", "12345", 0, 500)]  
        //[InlineData(Messages.InvalidSecurityQuantity, "123456789", "12345", -1, 500)]
        //[InlineData(Messages.InvalidWithdrawalQuantity, "123456789", "12345", 1000, 0)]
        //[InlineData(Messages.InvalidWithdrawalQuantity, "123456789", "12345", 1000, -1)]
        public async Task ShouldReturnFail_AfterHandle_InvalidRequestParameters() //, string customerId, string securityId, decimal securityQuantity, decimal withdrawalQuantity)
        {
            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();
            var request = new ReportQuery(checkBoxes);
            Response<ReportResponse> response = await _handler.Handle(request, CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            //response.Messages.Contains(expected).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_ThrowException()
        {
            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Throws(new Exception());
            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();
            Response<ReportResponse> response = await _handler.Handle(new ReportQuery(checkBoxes), CancellationToken.None);

            response.IsFailure.Should().BeTrue();
        }
    }
}
