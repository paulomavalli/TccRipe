//using RIPE.Application.Interfaces.Repository;
//using RIPE.Application.Queries;
//using RIPE.Application.QueryHandlers;
//using RIPE.Application.Responses;
//using RIPE.Domain;
//using RIPE.Tests.Fake;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using NSubstitute;
//using NSubstitute.ExceptionExtensions;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace RIPE.Tests.Application.QueryHandlers
//{
//    public class CollateralQueryHandlerTests
//    {
//        private readonly IRipeRepository _collateralRepository;
//        private readonly RipeQueryHandler _handler;
//        public CollateralQueryHandlerTests()
//        {
//            var logger = Substitute.For<ILogger<CollateralQueryHandler>>();
//            _collateralRepository = Substitute.For<IRipeRepository>();
//            _handler = new CollateralQueryHandler(logger, _collateralRepository);
//        }

//        private const string VALID_CUSTOMER_ID = "99999999999";
//        private const string VALID_SECURITY_ID = "1615342";


//        [Fact]
//        public async Task ShouldReturnFailure_NullRequest()
//        {
//            Response<CollateralResponse> response = await _handler.Handle(null, CancellationToken.None);

//            response.IsFailure.Should().BeTrue();
//            response.Messages.Contains(Messages.InvalidRequest).Should().BeTrue();
//        }


//        [Fact]
//        public async Task ShouldReturnSuccess_RealCollateral()
//        {
//            List<RIPE.Domain.Domains.Collateral> securityId = CollateralFake.GetRealCollateralSameSecurityId();
//            List<RIPE.Domain.Domains.Collateral> collateral = CollateralFake.GetRealCollateral();

//            _collateralRepository.GetCollateral(Arg.Any<string>()).Returns(collateral);
//            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Returns(securityId);

//            var request = new CollateralQuery(VALID_CUSTOMER_ID);
//            Response<CollateralResponse> response = await _handler.Handle(request, CancellationToken.None);

//            response.IsSuccess.Should().BeTrue();
//        }

//        [Fact]
//        public async Task ShouldReturnSuccess_NullCollateral()
//        {

//            _collateralRepository.GetCollateral(Arg.Any<string>()).Returns((List<RIPE.Domain.Domains.Collateral>)null);

//            var request = new CollateralQuery(VALID_CUSTOMER_ID);
//            Response<CollateralResponse> response = await _handler.Handle(request, CancellationToken.None);

//            response.IsSuccess.Should().BeTrue();
//        }

//        [Fact]
//        public async Task ShouldReturnSuccess_EmptyCollateral()
//        {

//            _collateralRepository.GetCollateral(Arg.Any<string>()).Returns(new List<RIPE.Domain.Domains.Collateral>());

//            var request = new CollateralQuery(VALID_CUSTOMER_ID);
//            Response<CollateralResponse> response = await _handler.Handle(request, CancellationToken.None);

//            response.IsSuccess.Should().BeTrue();
//        }

//        [Fact]
//        public async Task ShouldReturnFailure_ThrownException_GetCollateral()
//        {

//            _collateralRepository.GetCollateral(Arg.Any<string>()).Throws(new Exception());

//            var request = new CollateralQuery(VALID_CUSTOMER_ID);
//            Response<CollateralResponse> response = await _handler.Handle(request, CancellationToken.None);

//            response.IsFailure.Should().BeTrue();
//            response.ErrorResponse.Error.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

//        }

//        [Fact]
//        public async Task ShouldReturnFailure_ThrownException_GetCollateralPerSecurityId()
//        {

//            _collateralRepository.GetCollateralPerSecurityId(Arg.Any<string>(), Arg.Any<string>()).Throws(new Exception());

//            var request = new CollateralQuery(VALID_CUSTOMER_ID);
//            Response<CollateralResponse> response = await _handler.Handle(request, CancellationToken.None);

//            response.IsFailure.Should().BeTrue();
//            response.ErrorResponse.Error.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

//        }
//    }
//}
