using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Queries;
using RIPE.Application.QueryHandlers;
using RIPE.Application.Responses;
using RIPE.Domain;
using RIPE.Tests.Fake;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using RIPE.Domain.Domains.Questions;

namespace RIPE.Tests.Application.QueryHandlers
{
    public class SurveyQueryTests
    {

        private readonly IRipeRepository _collateralRepository;
        private readonly QuestionsQueryHandler _handler;

        public SurveyQueryTests()
        {
            _collateralRepository = Substitute.For<IRipeRepository>();
            var logger = Substitute.For<ILogger<QuestionsQueryHandler>>();

            _handler = new QuestionsQueryHandler(logger,
                _collateralRepository);
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsNull()
        {
            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Returns((IEnumerable<RIPE.Domain.Domains.Collateral>)null);

            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();
            var response = await _handler.Handle(new CollateralExceededByValueQuery(checkBoxes), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.survey.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsEmpty()
        {
            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Returns(new List<RIPE.Domain.Domains.Collateral>());

            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();
            Response<QuestionsResponse> response = await _handler.Handle(new CollateralExceededByValueQuery(checkBoxes), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.survey.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsLessThanWithdrawalQuantity()
        {
            const string securityId = "12345";
            const decimal quantityInCollateral = 5000.231M;

            List<RIPE.Domain.Domains.Collateral> collaterals = CollateralFake.GetRealCollateralSameSecurityId(securityId, quantityInCollateral);
            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Returns(collaterals);

            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();

            Response<QuestionsResponse> response = await _handler.Handle(new CollateralExceededByValueQuery(checkBoxes), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.survey.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_WithEmptyRequest()
        {
            Response<QuestionsResponse> response = await _handler.Handle((CollateralExceededByValueQuery)null, CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            response.Messages.Contains(Messages.InvalidRequest).Should().BeTrue();
        }

        [Theory]
        [InlineData(Messages.InvalidCustomerId, "", "12345", 20000, 1000, 500)]
        [InlineData(Messages.InvalidSecurityId, "123456789", "", 20000, 1000, 500)]
        [InlineData(Messages.InvalidSecurityNetValue, "123456789", "12345", 0, 1000, 500)]
        [InlineData(Messages.InvalidSecurityNetValue, "123456789", "12345", -20000, 1000, 500)]
        [InlineData(Messages.InvalidSecurityQuantity, "123456789", "12345", 20000, 0, 500)]
        [InlineData(Messages.InvalidSecurityQuantity, "123456789", "12345", 20000, -1, 500)]
        [InlineData(Messages.InvalidWithdrawalValue, "123456789", "12345", 20000, 1000, 0)]
        [InlineData(Messages.InvalidWithdrawalValue, "123456789", "12345", 20000, 1000, -1)]
        public async Task ShouldReturnFail_AfterHandle_InvalidRequestParameters(string expected, string customerId, string securityId, decimal securityNetValue, decimal securityQuantity, decimal withdrawalValue)
        {
            var checkBoxes = new List<TypeQuestions>();
            new TypeQuestions(customerId);
            new TypeQuestions(securityId);
            new TypeQuestions(securityQuantity.ToString());
            new TypeQuestions(securityNetValue.ToString());
            new TypeQuestions(withdrawalValue.ToString());

            var request = new CollateralExceededByValueQuery(checkBoxes);
            Response<QuestionsResponse> response = await _handler.Handle(request, CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            response.Messages.Contains(expected).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_ThrowException()
        {
            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Throws(new Exception());

            List<TypeQuestions> checkBoxes = new List<TypeQuestions>();

            var response = await _handler.Handle(new CollateralExceededByValueQuery(checkBoxes), CancellationToken.None);

            response.IsFailure.Should().BeTrue();
        }
    }
}
