using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal class Addition : BinaryExpression
    {
        public Addition(Expression first, Expression second)
            : base(first, second)
        {
        }

        public override Expression Derivative()
        {
            return FirstExpression.Derivative().Add(SecondExpression.Derivative());
        }

        public override double Calculate(double? point = null)
        {
            return FirstExpression.Calculate(point) + SecondExpression.Calculate(point);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("( ");
            sb.Append(FirstExpression);
            sb.Append(" + ");
            sb.Append(SecondExpression);
            sb.Append(" )");
            return sb.ToString();
        }

        public static Expression AdditionHandler(Expression expr1, Expression expr2) 
        {
            if (expr1 is Constant)
            {
                Constant TempExpr1 = (Constant)expr1;
                if (TempExpr1.Calculate() == 0)
                {
                    return expr2;
                }
                else if (expr2 is Constant)
                {
                    Constant TempExpr2 = (Constant)expr2;
                    double TemporaryValue = TempExpr1.Calculate() + TempExpr2.Calculate();
                    return Constant.GetConstant(TemporaryValue);
                }
            }
            else if (expr2 is Constant)
            {
                Constant TempExpr2 = (Constant)expr2;
                if (TempExpr2.Calculate() == 0)
                {
                    return expr1;
                }
            }
            else
            {
                return new Addition(expr1, expr2);
            }
            return new Addition(expr1, expr2);
        }
        public static Expression AdditionTimesAddition(Addition expr1, Addition expr2)
        {
            Expression FirstTimesFirst = expr1.FirstExpression.Multiply(expr2.FirstExpression);
            Expression FirstTimesSecond = expr1.FirstExpression.Multiply(expr2.SecondExpression);
            Expression SecondTimesFirst = expr1.SecondExpression.Multiply(expr2.FirstExpression);
            Expression SecondTimesSecond = expr1.SecondExpression.Multiply(expr2.SecondExpression);
            return FirstTimesFirst.Add(FirstTimesSecond.Add(SecondTimesFirst.Add(SecondTimesSecond)));
        }
        public static Expression AdditionTimesExpression(Addition expr1, Expression expr2)
        {
            return (expr1.FirstExpression.Multiply(expr2)).Add(expr1.SecondExpression.Multiply(expr2));
        }
    }
}

 


    
