using System;
using Xunit;
using FluentAssertions;
using NSubstitute;
using FunctionLibrary;

namespace FunctionLibrary.Tests
{
    public class StringCalculatorTests
    {
        [Theory]
        // Application must return zero if the input string is empty or null.
        [InlineData("", 0)]
        [InlineData("5", 5)]
        // Application must recognize addition and subtraction operators.
        [InlineData("2+2", 4)]
        [InlineData("3-1", 2)]
        [InlineData("3-2+4", 5)]
        // Application must recognize multiplication and division operators.
        [InlineData("5*5", 25)]
        [InlineData("4/2", 2)]
        [InlineData("10/2*6", 30)]
        [InlineData("5*4-6/3+4", 22)]
        // Application must allow and ignore spaces in the input string.
        [InlineData("5 +  10 /2", 10)]
        public void Calculate_ShouldCalculateStringExpression(string expression, double expectedResult)
        {
            // Arrange

            //Act
            var result = StringCalculator.Calculate(expression);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Calculate_ShouldThrowExceptionWithInvalidInput()
        {
            // Arrange
            string invalidInput = "9+9#5";

            // Act
            Action act = () => StringCalculator.Calculate(invalidInput);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("The provided expression string contains invalid characters (Parameter 'expression')");
        }
    }
}
