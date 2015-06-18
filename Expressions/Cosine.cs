using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal class Cosine : Expression
    {
        private Expression _expr;
        public Cosine(Expression first)
        {
            _expr = first;
        }
        public override Expression Derivative()
        {
            //return (Constant.GetConstant(1) / _expr) * (_expr.Derivative());
            return (Constant.GetConstant(-1) * _expr.Sine()) * _expr.Derivative();
        }

        public override double Calculate(double? point = null)
        {
            //return Math.Log(_expr.Calculate(point), Math.E);
            return Math.Cos(_expr.Calculate(point));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            //sb.Append(FirstExpression);
            sb.Append("Cos");
            sb.Append("( ");
            sb.Append(_expr);
            sb.Append(" )");
            return sb.ToString();
        }
    }
}