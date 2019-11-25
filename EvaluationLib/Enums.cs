using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationLib
{
    public class Enums
    {
        public enum Priority {
           MultDiv = 1,
           PlusMinus = 2,
           Default = 0
        }

        public enum Operand {
            Division = '/',
            Multiplication = '*',
            Subtraction = '-',
            Addition = '+'
        }
    }
}
