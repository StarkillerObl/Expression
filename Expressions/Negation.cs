using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal class Negation : Expression
    {
        private Expression _expr;
        public Negation(Expression first)
        {
            _expr = first;
        }       
        public override Expression Derivative()
        {
            return !(_expr.Derivative());
        }

        public override double Calculate(double? point = null)
        {
            return -(_expr.Calculate(point)); 
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            //sb.Append(FirstExpression);
            sb.Append("NEG");
            sb.Append("( ");
            sb.Append(_expr);
            sb.Append(" )");
            return sb.ToString();
        }
    }
}