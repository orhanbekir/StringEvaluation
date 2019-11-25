using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationLib
{
    interface IEvaluation
    {
        double EvaluateExpression(string expression);
        bool IsNumber(char ch);
        bool IsPriority(char currentOperant, char prevOperant);
        int GetOperandPriority(char ch);
        double Calculate(char oprator, double secondNumber, double firstNumber);

    }
}
