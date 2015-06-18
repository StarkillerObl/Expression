using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal class NaturalLogarithm : Expression
    {
        private Expression _expr;
        public NaturalLogarithm(Expression first)
        {
            _expr = first;
        }
        public override Expression Derivative()
        {
            return ((Expression)Constant.GetConstant(1)/_expr)*(_expr.Derivative());
        }

        public override double Calculate(double? point = null)
        {
            return Math.Log(_expr.Calculate(point), Math.E);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            //sb.Append(FirstExpression);
            sb.Append("ln");
            sb.Append("( ");
            sb.Append(_expr);
            sb.Append(" )");
            return sb.ToString();
        }
    }
}