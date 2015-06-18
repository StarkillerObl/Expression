using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal class Sine : Expression
    {
        private Expression _expr;
        public Sine(Expression first)
        {
            _expr = first;
        }
        public override Expression Derivative()
        {
            //return (Constant.GetConstant(1) / _expr) * (_expr.Derivative());
            return _expr.Cosine() * _expr.Derivative();
        }

        public override double Calculate(double? point = null)
        {
            return Math.Sin(_expr.Calculate(point));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            //sb.Append(FirstExpression);
            sb.Append("Sin");
            sb.Append("( ");
            sb.Append(_expr);
            sb.Append(" )");
            return sb.ToString();
        }
    }
}