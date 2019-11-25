using EvalutonCalculatorApi.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationLib
{
    public class Evaluation : IEvaluation
    {
        private readonly string availableOperands = "+-*/";//Default Operand
        /// <summary>
        /// Pass Emtry For Default Operands +,-,*,/ 
        /// </summary>
        /// <param name="availableOps"></param>
        public Evaluation(string availableOps)
        {
            if (!String.IsNullOrEmpty(availableOps))
            {
                availableOperands = availableOps;
            }
        }

        /// <summary>
        /// Evaluate String Expression with in +-*/ without space and pharantesis
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public double EvaluateExpression(string expression)
        {
            Stack<double> numbers = new Stack<double>();//Stored our numbers
            Stack<char> operators = new Stack<char>();//Stored our operands
            char[] tokens = expression.Trim().ToCharArray();

            for (int i = 0; i < tokens.Length; i++)
            {
                if (IsNumber(tokens[i]))//Numbers
                {
                    StringBuilder numberBuilder = new StringBuilder();
                    //Generation numbers more than 9
                    while (i < tokens.Length && IsNumber(tokens[i]))
                    {
                        numberBuilder.Append(tokens[i++]);
                    }
                    i--;
                    numbers.Push(int.Parse(numberBuilder.ToString()));
                }
                else if (availableOperands.Contains(tokens[i]))//Operands
                {
                    while (operators.Count > 0 && IsPriority(tokens[i], operators.Peek()))
                    {
                        numbers.Push(Calculate(operators.Pop(), numbers.Pop(), numbers.Pop()));
                    }
                    operators.Push(tokens[i]);
                }
                else//Invalid operand or character
                {
                    throw new InvalidTokenException(tokens[i]);
                }
            }

            //Calculate incompleted operation
            while (operators.Count > 0)
            {
                numbers.Push(Calculate(operators.Pop(), numbers.Pop(), numbers.Pop()));
            }
            return numbers.Pop();//Remaining number on the number stack is our result
        }

        /// <summary>
        /// Is a number return true alse return false
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool IsNumber(char ch)
        {
            int number;
            return Int32.TryParse(ch.ToString(), out number);
        }

        /// <summary>
        /// Is Current Operant Has a Priority Return false else return true
        /// </summary>
        /// <param name="currentOperant"></param>
        /// <param name="prevOperant"></param>
        /// <returns></returns>
        public bool IsPriority(char currentOperant, char prevOperant)
        {
            if (GetOperandPriority(currentOperant) > GetOperandPriority(prevOperant))
                return true;
            else if (GetOperandPriority(currentOperant) < GetOperandPriority(prevOperant))
                return false;

            return true;
        }

        public int GetOperandPriority(char ch)
        {
            if (ch == (char)Enums.Operand.Multiplication || ch == (char)Enums.Operand.Division)
                return (int)Enums.Priority.MultDiv;
            else if (ch == (char)Enums.Operand.Addition || ch == (char)Enums.Operand.Subtraction)
                return (int)Enums.Priority.PlusMinus;
            else
                return (int)Enums.Priority.Default;
        }

        /// <summary>
        /// Calculates Two numbers with operands
        /// </summary>
        /// <param name="oprator"></param>
        /// <param name="secondNumber"></param>
        /// <param name="firstNumber"></param>
        /// <returns></returns>
        public double Calculate(char oprator, double secondNumber, double firstNumber)
        {
            switch (oprator)
            {
                case (char)Enums.Operand.Addition:
                    return firstNumber + secondNumber;
                case (char)Enums.Operand.Subtraction:
                    return firstNumber - secondNumber;
                case (char)Enums.Operand.Multiplication:
                    return firstNumber * secondNumber;
                case (char)Enums.Operand.Division:
                    if (secondNumber == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return firstNumber / secondNumber;
            }
            return 0;
        }
    }
}
