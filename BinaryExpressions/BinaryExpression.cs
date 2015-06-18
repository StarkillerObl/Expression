using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal abstract class BinaryExpression : Expression
    {
        protected readonly Expression FirstExpression, SecondExpression;

        protected BinaryExpression(Expression first, Expression second)
        {
            FirstExpression = first;
            SecondExpression = second;
        }

        public Expression ReturnFirstExpression()
        {
            return FirstExpression;
        }
        public Expression ReturnSecondExpression()
        {
            return SecondExpression;
        }
    }
}