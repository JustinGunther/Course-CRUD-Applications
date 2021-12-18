using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionLibrary
{
    public static class StringCalculator
    {
        private static readonly char[] ValidChars = new char[]
        { 
            '+',
            '-',
            '*',
            '/',
            '(',
            ')',
            '^'
        };

        public static double Calculate(string expression)
        {
            double result = 0;

            if (!String.IsNullOrEmpty(expression))
            {
                result = _Add(expression);
            }

            return result;
        }

        private static double _Multiply(string expression)
        {
            string[] multiplyItems = expression.Split('*');
            List<double> numbersToMultiply = new List<double>();
            double result = 0;

            foreach (string item in multiplyItems)
            {
                if (item.Contains("/"))
                {   
                    numbersToMultiply.Add(_Divide(item));
                }
                else
                {
                    double currentNumber;
                    if (!double.TryParse(item, out currentNumber))
                    {
                        throw new ArgumentOutOfRangeException("expression", "The provided expression string contains invalid characters");
                    }

                    numbersToMultiply.Add(currentNumber);
                }
            }

            for (int i = 0; i < numbersToMultiply.Count; i++)
            {
                if (i == 0)
                {
                    result = numbersToMultiply[i];
                }
                else
                {
                    result = result * numbersToMultiply[i];
                }
            }

            return result;
        }

        private static double _Divide(string expression)
        {
            string[] divisionItems = expression.Split("/");
            List<double> numbersToDivide = new List<double>();
            double divisionTotal = 0;

            foreach (string divisionItem in divisionItems)
            {
                double currentNumber = 0;
                if (!double.TryParse(divisionItem, out currentNumber))
                {
                    throw new ArgumentOutOfRangeException("expression", "The provided expression string contains invalid characters");
                }

                numbersToDivide.Add(currentNumber);
            }
            
            for (int i = 0; i < numbersToDivide.Count; i++)
            {
                if (i == 0)
                {
                    divisionTotal = numbersToDivide[i];
                }
                else
                {
                    divisionTotal = divisionTotal / numbersToDivide[i];
                }
            }

            return divisionTotal;
        }

        private static double _Add(string expression)
        {
            string[] operands = expression.Split('+');
            List<double> numbersToSum = new List<double>();

            foreach (string item in operands)
            {
                double number = 0;

                if (item.Contains("-"))
                {
                    number = _Subtract(item);
                }
                else
                {
                    number = _Multiply(item);
                }

                numbersToSum.Add(number);
            }

            return numbersToSum.Sum();
        }

        private static double _Subtract(string expression)
        {
            string subtractionString = expression;

            if (expression.IndexOf('-') == 0)
            {
                subtractionString = expression.Insert(0, "0");
            }

            string[] subOperands = subtractionString.Split('-');
            double runningTotal = 0;

            for (int i = 0; i < subOperands.Length; i++)
            {
                if (i == 0)
                {
                    runningTotal = _Multiply(subOperands[i]);
                }
                else
                {
                    double currentValue = _Multiply(subOperands[i]);

                    runningTotal = runningTotal - currentValue;
                }
            }

            return runningTotal;
        }
    }
}
