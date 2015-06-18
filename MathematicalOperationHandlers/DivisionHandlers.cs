using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary;
using ExpressionClassLibrary.Expressions;
using ExpressionClassLibrary.BinaryExpressions;

namespace ExpressionClassLibrary.MathematicalOperationHandlers
{
    public class DivisionHandlers
    {
        public static Expression MainDivisionHandler(Expression expr1, Expression expr2)
        {
            string Expr1Type = HelperMethods.ExpressionType(expr1).ToLower();
            string Expr2Type = HelperMethods.ExpressionType(expr2).ToLower();
            switch (Expr1Type)
            {
                case "constant":
                    switch (Expr2Type)
                    {
                        case "variable":
                            return expr1 * (new Power(expr2, Constant.GetConstant(-1)));
                    }
                    break;
            }
            return new Division(expr1, expr2);
        }
    }
}
