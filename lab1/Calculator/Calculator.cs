using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculator
    {
        private enum Operation
        {
            Add = 0,
            Sub = 1,
            Mul = 2,
            Div = 3
        }

        static void Main(string[] args)
        {
            WriteHeader();

            try
            {
                double num1 = ReadNumber("Enter first number");
                double num2 = ReadNumber("Enter second number");
                Operation op = ReadOperation("Enter operation: + - * or /");
                double res = Calculate(num1, num2, op);
                WriteResult(num1, num2, op, res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            WriteFooter();
        }

        static double ReadNumber(string msg)
        {
            Console.WriteLine(msg);

            try
            {
                return Convert.ToDouble(Console.ReadLine());
            }
            catch (FormatException)
            {
                throw new FormatException("Failed to read number: string contains non numeric symbols.");
            }
            catch (OverflowException)
            {
                throw new OverflowException("Failed to read number: value overflow.");
            }
        }

        static Operation ReadOperation(string msg)
        {
            Console.WriteLine(msg);

            string operationChar = Console.ReadLine();

            switch (operationChar)
            {
                case "+":
                    return Operation.Add;
                case "-":
                    return Operation.Sub;
                case "*":
                    return Operation.Mul;
                case "/":
                    return Operation.Div;
                default:
                    throw new ArgumentException($"Invalid operation. Expect: + - * /. Got: {operationChar}");
            }
        }

        static double Calculate(double num1, double num2, Operation operation)
        {
            double result;

            switch (operation)
            {
                case Operation.Add:
                    result = num1 + num2;
                    break;
                case Operation.Sub:
                    result = num1 - num2;
                    break;
                case Operation.Mul:
                    result = num1 * num2;
                    break;
                case Operation.Div:
                    result = num1 / num2;
                    break;
                default:
                    result = double.NaN;
                    break;
            }

            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                throw new ArithmeticException("Failed to calculate expression. Result value overflow or diving by zero.");
            }

            return result;
        }

        static void WriteResult(double num1, double num2, Operation operation, double result)
        {
            char operationChar;

            switch (operation)
            {
                case Operation.Add:
                    operationChar = '+';
                    break;
                case Operation.Sub:
                    operationChar = '-';
                    break;
                case Operation.Mul:
                    operationChar = '*';
                    break;
                case Operation.Div:
                    operationChar = '/';
                    break;
                default:
                    throw new ArgumentException("Invalid operation");
            }

            Console.WriteLine($"The result of {num1} {operationChar} {num2} is {result}");
        }

        static void WriteHeader()
        {
            Console.WriteLine("Calculator C# console application");
            Console.WriteLine("---------------------------------");
        }

        static void WriteFooter()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Press any key and exit program...");
            Console.ReadKey();
        }
    }
}
