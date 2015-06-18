using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal class Power : BinaryExpression
    {
        public Power(Expression first, Expression second)
            : base(first, second)
        {
        }

        public override Expression Derivative()
        {
            //return FirstExpression.Derivative()
            //                   .Multiply(SecondExpression)
            //                   .Add(SecondExpression.Derivative().Multiply(FirstExpression));
            //if ()
            return FirstExpression.Power(SecondExpression) * ((FirstExpression.Ln() * SecondExpression).Derivative());

        }

        public override double Calculate(double? point = null)
        {
            return Math.Pow(FirstExpression.Calculate(point), SecondExpression.Calculate(point));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("( ");
            sb.Append(FirstExpression);
            sb.Append(" ^ ");
            sb.Append(SecondExpression);
            sb.Append(" )");
            return sb.ToString();
        }
    }
}