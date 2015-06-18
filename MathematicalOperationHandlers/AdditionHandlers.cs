using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary;
using ExpressionClassLibrary.Expressions;
using ExpressionClassLibrary.BinaryExpressions;

namespace ExpressionClassLibrary.MathematicalOperationHandlers
{
    public class AdditionHandlers
    {
        public static Expression MainAdditionHandler(Expression expr1, Expression expr2)
        {
            string Expr1Type = HelperMethods.ExpressionType(expr1).ToLower();
            string Expr2Type = HelperMethods.ExpressionType(expr2).ToLower();
            switch (Expr1Type)
            {
                case "constant":
                    Constant TempExpr1 = (Constant)expr1;
                    if (TempExpr1.Calculate() == 0)
                    {
                        return expr2;
                    }
                    else
                    {
                        switch (Expr2Type)
                        {

                            case "constant":
                                return ConstantPlusConstant((Constant)expr1, (Constant)expr2);
                            case "cosine":
                                return new Addition(expr2, expr1);
                            case "naturallogarithm":
                                return new Addition(expr2, expr1);
                            case "negation":
                                return new Addition(expr2, expr1);
                            case "sine":
                                return new Addition(expr2, expr1);
                            case "variable":
                                return new Addition(expr2, expr1);
                            case "addition":
                                return ConstantPlusAddition((Constant)expr1, (Addition)expr2);
                            case "division":
                                //return ConstantTimesDivision((Constant)expr1, (Division)expr2);
                                return new Addition(expr2, expr1);
                            case "multiplication":
                                //return ConstantTimesMultiplication((Constant)expr1, (Multiplication)expr2);
                                return new Addition(expr2, expr1);
                                //return new ConstantPlusMultiplication((Constant)expr1, (Multiplication)expr2);
                            case "power":
                                //return ConstantTimesPower((Constant)expr1, (Power)expr2);
                                return new Addition(expr2, expr1);
                            case "substraction":
                                //return ConstantPlusSubstraction((Constant)expr1, (Substraction)expr2);
                                return new Addition(expr2, expr1);
                        }
                    }
                    break;
                //case "cosine":
                //                switch (Expr2Type)
                //                {
                //                    case "constant":
                //                        return expr2 * expr1;
                //                    case "cosine":
                //                        return 
                //                }
                case "variable":
                    switch (Expr2Type)
                    {
                        case "constant":
                            return expr2 + expr1;
                        case "variable":
                            //return new Power(Variable.GetVariable, Constant.GetConstant(2));
                            return VariablePlusVariable((Variable)expr1, (Variable)expr2);
                        case "addition":
                            return VariablePlusAddition((Variable)expr1, (Addition)expr2);
                        case "division":
                            //return VariableTimesDivision((Variable)expr1, (Division)expr2);
                            return new Addition(expr1, expr2);
                        case "multiplication":
                            return VariablePlusMultiplication((Variable)expr1, (Multiplication)expr2);
                        case "power":
                            //return VariableTimesPower((Variable)expr1, (Power)expr2);
                            return new Addition(expr1, expr2);
                        case "substraction":
                            //return VariablePlusSubstraction((Variable)expr1, (Substraction)expr2);
                            return new Addition(expr1, expr2);

                    }
                    break;
                case "addition":
                    switch (Expr2Type)
                    {
                        case "constant":
                            return ConstantPlusAddition((Constant)expr2, (Addition)expr1);
                        case "variable":
                            return VariablePlusAddition((Variable)expr2, (Addition)expr1);
                        //case "addition":
                        //    return AdditionPlusAddition((Addition)expr1, (Addition)expr2);
                    }
                    break;
                case "multiplication":
                    switch (Expr2Type)
                    {
                        case "constant":
                            //return ConstantPlusMultiplication((Constant)expr2, (Multiplication)expr1);
                            return expr2 + expr1;
                        case "variable":
                            return VariablePlusMultiplication((Variable)expr2, (Multiplication)expr1);
                    }
                    break;
                //case "division":
                //    switch (Expr2Type)
                //    {
                //        case "constant":
                //            //return ConstantTimesDivision((Constant)expr2, (Division)expr1);
                //            return new Addition(expr1, expr2);
                //        case "variable":
                //            return expr2 + expr1;
                //    }
                //    break;
                //case "power":
                //    switch (Expr2Type)
                //    {
                //        case "constant":
                //            return expr2 + expr1;
                //        case "variable":
                //            return expr2 + expr1;
                //        case "power":
                //            return PowerPlusPower((Power)expr1, (Power)expr2);
                //    }
                //    break;

            }
            return new Addition(expr1, expr2);
        }
        #region ConstantPlus
        private static Expression ConstantPlusConstant(Constant constant1, Constant constant2)
        {
            double constant1Calculate = constant1.Calculate();
            double constant2Calculate = constant2.Calculate();
            return Constant.GetConstant(constant1Calculate + constant2Calculate);
        }
        private static Expression ConstantPlusAddition(Constant constant, Addition addition)
        {
            Expression addition1 = addition.ReturnFirstExpression();
            Expression addition2 = addition.ReturnSecondExpression();
            string addition1Type = HelperMethods.ExpressionType(addition1).ToLower();
            string addition2Type = HelperMethods.ExpressionType(addition2).ToLower();

            if (addition2Type == "constant")
            {
                return new Addition(addition1, constant + addition2);
            }
            return new Addition(addition, constant);
        }
        #endregion
        #region VariablePlus
        private static Expression VariablePlusVariable(Variable variable1, Variable variable2)
        {
            return new Multiplication(Constant.GetConstant(2), Variable.GetVariable);
        }
        private static Expression VariablePlusAddition(Variable variable, Addition addition)
        {
            Expression addition1 = addition.ReturnFirstExpression();
            Expression addition2 = addition.ReturnSecondExpression();

            string addition1Type = HelperMethods.ExpressionType(addition1).ToLower();
            string addition2Type = HelperMethods.ExpressionType(addition2).ToLower();

            switch (addition1Type)
            {
                case "variable":
                    return new Addition(variable + addition1, addition2);
                case "addition":
                    return new Addition(variable + addition1, addition2);
                case "multiplication":
                    return new Addition(variable + addition1, addition2);
            }
            return new Addition(variable, addition);
        }
        private static Expression VariablePlusMultiplication(Variable variable, Multiplication multiplication)
        {
            Expression multiplication1 = multiplication.ReturnFirstExpression();
            Expression multiplication2 = multiplication.ReturnSecondExpression();

            string multiplication1Type = HelperMethods.ExpressionType(multiplication1).ToLower();
            string multiplication2Type = HelperMethods.ExpressionType(multiplication2).ToLower();

            if (multiplication2Type == "variable")
            {
                return new Multiplication(multiplication1 + Constant.GetConstant(1), multiplication2);
            }
            else if (multiplication1Type == "variable")
            {
                return new Multiplication(multiplication2, multiplication1 + Constant.GetConstant(1));
            }
            return new Addition(variable, multiplication);

        }
        #endregion
    }
}
