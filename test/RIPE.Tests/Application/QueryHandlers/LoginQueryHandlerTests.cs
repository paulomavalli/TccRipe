using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Application.Queries;
using RIPE.Application.QueryHandlers;
using RIPE.Application.Responses;
using RIPE.Domain;
using RIPE.Domain.Domains;
using RIPE.Tests.Fake;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RIPE.Tests.Application.QueryHandlers
{
    public class LoginQueryHandlerTests
    {
        //private readonly IRipeRepository _ripeRepository;
        private readonly LoginQueryHandler _handler;
        private readonly IReadCacheRepository _readCacheRepository;
        //  private readonly IReportService _reportService;

        public LoginQueryHandlerTests()
        {
            _readCacheRepository = Substitute.For<IReadCacheRepository>();
            var logger = Substitute.For<ILogger<LoginQueryHandler>>();

            _handler = new LoginQueryHandler(logger,
                                            _readCacheRepository
                                            );

        }

        private const string VALID_LOANVALUE = "password";

        #region Handle
        [Fact]
        public async Task ShouldReturnFail_AfterHandle_PrioritiesNull()
        {
            // _collateralPriorityRepository.GetCollateralPriority().Returns((IEnumerable<CollateralPriority>)null);

            Response<ValidateLoginResponse> response = await _handler.Handle(new ValidateLoginQuery("login", "senha"), CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            response.Messages.Contains(Messages.UnavailableCollateralPriorityList).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_PrioritiesEmpty()
        {
            // // _collateralPriorityRepository.GetCollateralPriority().Returns(new List<CollateralPriority>());



            Response<ValidateLoginResponse> response = await _handler.Handle(new ValidateLoginQuery("login", VALID_LOANVALUE), CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            response.Messages.Contains(Messages.UnavailableCollateralPriorityList).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_CollateralPriorityQueryNull()
        {
            Response<ValidateLoginResponse> response = await _handler.Handle((ValidateLoginQuery)null, CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            response.Messages.Contains(Messages.InvalidRequest).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_CollateralPriorityListNull()
        {
            Response<ValidateLoginResponse> response = await _handler.Handle(new ValidateLoginQuery(null, "senha"), CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            response.Messages.Contains(Messages.InvalidLogin).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_LoanValueZero()
        {
            List<ProductRequest> productsRequest = ProductFake.GetRealProductsRequestList();
            Response<ValidateLoginResponse> response = await _handler.Handle(new ValidateLoginQuery("login", null), CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            response.Messages.Contains(Messages.InvalidPassword).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnNecessaryProducts_AfterHandle_NecessaryProducts()
        {
            //List<ProductResponse> productsResponse = ProductFake.GetRealProductsResponseList();
            //List<ProductRequest> productsRequest = ProductFake.GetRealProductsRequestList();
            //List<CollateralPriority> collateralPriority = CollateralPriorityFake.GetRealListPriorities();
            //List<ProductRequest> necessaryProducts = ProductFake.GetNecessaryProducts();

            //_collateralPriorityRepository.GetCollateralPriority().Returns(collateralPriority);
            //_collateralPriorityService.GetCollateralProducts(Arg.Any<List<ProductRequest>>(), Arg.Any<decimal>(), Arg.Any<List<CollateralPriority>>())
            //                          .Returns(necessaryProducts);

            Response<ValidateLoginResponse> response = await _handler.Handle(new ValidateLoginQuery("login", "senha"), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.sucess.Should().BeTrue();
        }

        #endregion

    }

}

