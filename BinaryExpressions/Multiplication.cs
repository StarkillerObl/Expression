using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary.BinaryExpressions
{
    internal class Multiplication : BinaryExpression
    {
        public Multiplication(Expression first, Expression second)
            : base(first, second)
        {
        }

        public override Expression Derivative()
        {
            return FirstExpression.Derivative()
                               .Multiply(SecondExpression)
                               .Add(SecondExpression.Derivative().Multiply(FirstExpression));
        }

        public override double Calculate(double? point = null)
        {
            return FirstExpression.Calculate(point) * SecondExpression.Calculate(point);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("( ");
            sb.Append(FirstExpression);
            sb.Append(" * ");
            sb.Append(SecondExpression);
            sb.Append(" )");
            return sb.ToString();
        }
        public static Expression MultiplicationHandler(Expression expr1, Expression expr2)
        {
            if (Expression.ExpressionEqualsExpression(expr1,expr2))
            {
                return new Power(expr1, Constant.GetConstant(2));
            }
            if (expr1 is Constant)
            {
                Constant TempExpr1 = (Constant)expr1;
                if (TempExpr1.Calculate() == 0)
                {
                    return Constant.GetConstant(0.0);
                }
                else if (TempExpr1.Calculate() == 1)
                {
                    return expr2;
                }
                else if (expr2 is Constant)
                {
                    Constant TempExpr2 = (Constant)expr2;
                    double TemporaryValue = TempExpr1.Calculate() * TempExpr2.Calculate();
                    return Constant.GetConstant(TemporaryValue);
                }
            }
            else if (expr1 is Multiplication)
            {
                Multiplication TempExpr1 = (Multiplication)expr1;
                //Expression TempExpr1FirstExpression = TempExpr1.FirstExpression;
                if (TempExpr1.FirstExpression is Constant)
                {
                    if (expr2 is Constant)
                    {
                        Constant TempExpr2 = (Constant)expr2;
                        //double TemporaryValue = TempExpr1FirstExpression.Calculate() * TempExpr2.Calculate();
                        //return new Multiplication(TempExpr1.FirstExpression.Multiply(TempExpr2), TempExpr1.SecondExpression);
                        return (TempExpr1.FirstExpression.Multiply(TempExpr2)).Multiply(TempExpr1.SecondExpression);
                    }
                    else if (expr2 is Multiplication)
                    {
                        Multiplication TempExpr2 = (Multiplication)expr2;
                        if (TempExpr2.FirstExpression is Constant)
                        {
                            //return new Multiplication(new Multiplication(TempExpr1.FirstExpression, TempExpr2.FirstExpression), new Multiplication(TempExpr1.SecondExpression, TempExpr2.SecondExpression));
                            return (((TempExpr1.FirstExpression.Multiply(TempExpr2.FirstExpression)).Multiply(TempExpr1.SecondExpression)).Multiply(TempExpr2.SecondExpression));
                        }
                    }
                }
            }
            else if (expr1 is Addition)
            {
                Addition TempExpr1 = (Addition)expr1;
                if (expr2 is Addition)
                {
                    Addition TempExpr2 = (Addition)expr2; 
                    //Multiplication FirstTimesFirst = new Multiplication(TempExpr1.First)
                    return Addition.AdditionTimesAddition(TempExpr1, TempExpr2);
                }
                //else 
            }
            else if (expr2 is Constant)
            {
                Constant TempExpr2 = (Constant)expr2;
                if (!expr1 is Constant)
                {
                    return expr2.Multiply(expr1);
                }
                if (TempExpr2.Calculate() == 0)
                {
                    return Constant.GetConstant(0.0);
                }
                else if (TempExpr2.Calculate() == 1)
                {
                    return expr1;
                    
                }
            }
            else
            {
                return new Multiplication(expr1, expr2);
            }
            return new Multiplication(expr1, expr2);
        }
    }
}