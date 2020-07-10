using RIPE.CrossCutting.Extensions;
using FluentAssertions;
using System.ComponentModel;
using Xunit;

namespace RIPE.Tests.Extensions
{
    public class EnumExtensionTests
    {
        private const string ENUM_DESCRIPTION = "Enum Value Description";
        private const string ENUM_DISPLAY_TEXT = "Enum Value Display text";

        private enum TestEnum
        {
            [Description(ENUM_DESCRIPTION)]
            ValueWithDescription,
            ValueWithoutDescription,
            ValueWithoutDisplayText,
            [DisplayText(ENUM_DISPLAY_TEXT)]
            ValueWithDisplayText
        }

        [Fact]
        public void Should_Return_Enum_Description()
        {
            var enumDescription = ((TestEnum)0).GetEnumDescription();
            enumDescription.Should().Be(enumDescription);
        }

        [Fact]
        public void Should_Return_Enum_Name()
        {
            var enumDescription = ((TestEnum)1).GetEnumDescription();
            enumDescription.Should().Be(((TestEnum)1).ToString());
            enumDescription = ((TestEnum)2).GetEnumDisplayText();
            enumDescription.Should().Be(((TestEnum)2).ToString());
        }

        [Fact]
        public void Should_Return_Enum_Display_Text()
        {
            var enumDescription = ((TestEnum)3).GetEnumDisplayText();
            enumDescription.Should().Be(enumDescription);
        }
    }
}
