using FluentAssertions;
using RIPE.CrossCutting.Extensions;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace RIPE.Tests.Extensions
{
    public class ClaimsPrincipalExtensionTests
    {
        [Fact]
        public void Should_Get_GivenName()
        {
            var givenName = "Test";
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            var claimsList = new List<Claim> { new Claim(ClaimTypes.GivenName, givenName) };
            claimsPrincipal.AddIdentity(new ClaimsIdentity(claimsList));

            var result = claimsPrincipal.GetGivenName();
            result.Should().Be(givenName);
        }

        [Fact]
        public void Should_Get_Null_GivenName()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            var result = claimsPrincipal.GetGivenName();

            result.Should().BeNull();
        }

        [Fact]
        public void Should_Get_Null_Username()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            var result = claimsPrincipal.GetUsername();

            result.Should().BeNull();
        }

        [Fact]
        public void Should_Get_Username()
        {
            var name = "Test";
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            var claimsList = new List<Claim> { new Claim(ClaimTypes.Name, name) };
            claimsPrincipal.AddIdentity(new ClaimsIdentity(claimsList));

            var result = claimsPrincipal.GetUsername();
            result.Should().Be(name);
        }

        [Fact]
        public void Should_Get_CustomerId()
        {
            var customerId = "123456789";
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            var claimsList = new List<Claim> { new Claim(ClaimTypes.Name, customerId) };
            claimsPrincipal.AddIdentity(new ClaimsIdentity(claimsList));

            var result = claimsPrincipal.GetCustomerId();
            result.Should().Be(customerId);
        }

        [Fact]
        public void Should_Get_Null_CustomerId()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            var result = claimsPrincipal.GetCustomerId();

            result.Should().BeNull();
        }

    }
}
