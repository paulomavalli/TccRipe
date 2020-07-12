using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Application.Queries;
using RIPE.Application.QueryHandlers;
using RIPE.Application.Responses;
using RIPE.Domain;
using RIPE.Domain.Domains;
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
        private readonly IReadCacheRepository _readCacheRepository;
        private readonly ReportQueryHandler _handler;

        public ReportQueryHandlerTests()
        {
            _readCacheRepository = Substitute.For<IReadCacheRepository>();
            var logger = Substitute.For<ILogger<ReportQueryHandler>>();

            _handler = new ReportQueryHandler(logger, _readCacheRepository);
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsNull()
        {
            _readCacheRepository.GetUser().Returns((IEnumerable<RIPE.Domain.Domains.ValidateUser>)null);

             
            Response<ReportResponse> response = await _handler.Handle(new ReportQuery("10","20","70"), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.NivelMaturidade.Should().Be("0");
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsEmpty()
        {
            _readCacheRepository.GetUser().Returns((new List<RIPE.Domain.Domains.ValidateUser>()));

             
            Response<ReportResponse> response = await _handler.Handle(new ReportQuery("10","20","70"), CancellationToken.None);

            response.IsSuccess.Should().BeTrue();
            response.Value.NivelMaturidade.Should().Be("0");
        }

        [Fact]
        public async Task ShouldReturnSuccess_AfterHandle_CollateralsLessThanWithdrawalQuantity()
        {
            //  const decimal securityQuantity = 10000.506M;
            // const decimal withdrawalQuantity = 6000.342M;

            //var collaterals = new List<ValidateUser>
            //{
            //    new ValidateUser("1", "securityId"),
            //    new UserDetails("2", "securityId2")
            //};

         //   _readCacheRepository.GetUser().Returns(collaterals);

             
            Response<ReportResponse> response = await _handler.Handle(new ReportQuery("10","20","70"), CancellationToken.None);

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
             
            var request = new ReportQuery("10","20","70");
            Response<ReportResponse> response = await _handler.Handle(request, CancellationToken.None);

            response.IsFailure.Should().BeTrue();
            //response.Messages.Contains(expected).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFail_AfterHandle_ThrowException()
        {
            _readCacheRepository.GetUser().Throws(new Exception());
             
            Response<ReportResponse> response = await _handler.Handle(new ReportQuery("10","20","70"), CancellationToken.None);

            response.IsFailure.Should().BeTrue();
        }
    }
}
