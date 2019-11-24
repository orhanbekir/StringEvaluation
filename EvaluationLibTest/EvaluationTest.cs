using System;
using EvaluationLib;
using EvalutonCalculatorApi.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EvaluationCalculatorApiTest
{
    [TestClass]
    public class EvaluationTest
    {
        private string operands = "*/+-";
        Evaluation eval;
        [TestMethod]
        public void EvaluateExpression_3_Plus_5_Mult_2_Result_13()
        {
            eval = new Evaluation(operands);
            double result = eval.EvaluateExpression("3+5*2");
            Assert.AreEqual(13,result);
        }

        [TestMethod]
        public void EvaluateExpression_3_Mult_5_Minus_6_Div_2_Plus_3_Result_15()
        {
            Evaluation eval = new Evaluation(operands);
            double result = eval.EvaluateExpression("3*5-6/2+3");
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void EvaluateExpression_Exception_Divide_By_Zero()
        {
            Evaluation eval = new Evaluation(operands);
            Assert.ThrowsException<DivideByZeroException>(() => {
                eval.EvaluateExpression("3*6/0");
            });
        }

        [TestMethod]
        public void IsNumber_5_True()
        {
            Evaluation eval = new Evaluation(operands);
            bool result = eval.IsNumber('5');
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsNumber_Plus_False()
        {
            Evaluation eval = new Evaluation(operands);
            bool result = eval.IsNumber('+');
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Calculate_Three_Multi_Five()
        {
            Evaluation eval = new Evaluation(operands);
            double result = eval.Calculate('*',3,5);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void IsPriority_Mult_Plus_False()
        {
            Evaluation eval = new Evaluation(operands);
            bool result = eval.IsPriority('*', '+');
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsPriority_Minus_Div_True()
        {
            Evaluation eval = new Evaluation(operands);
            bool result = eval.IsPriority('-', '/');
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EvaluateExpression_Exception_Invalid_Operand_Power()
        {
            Evaluation eval = new Evaluation(operands);
            Assert.ThrowsException<InvalidTokenException>(() => {
                eval.EvaluateExpression("3^5");
            });
        }
    }
}
