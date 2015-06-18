using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal class Division : BinaryExpression
    {
        public Division(Expression first, Expression second)
            : base(first, second)
        {
        }

        public override Expression Derivative()
        {
            return
                (FirstExpression.Derivative() * SecondExpression - FirstExpression * SecondExpression.Derivative())
                        / (SecondExpression * SecondExpression);
        }                   
        public override double Calculate(double? point = null)
        {
            return FirstExpression.Calculate(point) / SecondExpression.Calculate(point);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("( ");
            sb.Append(FirstExpression);
            sb.Append(" / ");
            sb.Append(SecondExpression);
            sb.Append(" )");
            return sb.ToString();
        }
    }
}