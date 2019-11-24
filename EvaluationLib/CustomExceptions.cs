using System;

namespace EvalutonCalculatorApi.CustomExceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(char token) : base($"Unrecognized token in expression: {token}")
        {
        }
    }


}