using RIPE.CrossCutting.Extensions;
using FluentAssertions;
using Xunit;

namespace RIPE.Tests.Extensions
{
    public class StringExtensionTests
    {
        [Fact]
        public void Should_Generate_Hash()
        {
            var listStrings = new[] { "teste", "66074485267", "84045579537", "02874249289", "43696507121", "70783071949" };
            var listHashes = new[] {"46070D4BF934FB0D4B06D9E2C46E346944E322444900A435D7D9A95E6D7435F5",
                "B29A69DFFC027A3FDCEF89E9E257DB4268E3DDC4F0DE6B27E058B7A3A93346FA",
                "F50E46DFA3D24CF14CEDFBB790633E0CCD3E576A5EA41B5ACB4DED2E5109B089",
                "8A7FCA7E9B8945A1DFF07AE0ADCEAAD04336E6BA7670DDA1B0795CA8BF62E6E5",
                "055DFE4CDD4C23925EB4EE2E9700A251F8A56A89D1B914B8F083914A2D62597F",
                "9dd95a22b3156e806b772dfe827290878886790783368db5990adddffc9e2e8b"};

            for (int i = 0; i < listStrings.Length; i++)
            {
                var hash = listStrings[i].GenerateSha256Hash();
                hash.Should().BeEquivalentTo(listHashes[i]);
            }
        }

        [Fact]
        public void Should_Return_Empty_Hash()
        {
            var emptyString = string.Empty;
            var hash = emptyString.GenerateSha256Hash();
            hash.Should().Be(string.Empty);

            string nullString = null;
            hash = nullString.GenerateSha256Hash();
            hash.Should().Be(string.Empty);
        }
    }
}
