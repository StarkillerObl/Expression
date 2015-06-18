using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary;
using ExpressionClassLibrary.Expressions;
using ExpressionClassLibrary.BinaryExpressions;

namespace ExpressionClassLibrary.MathematicalOperationHandlers
{
    public class MultiplicationHandlers
    {
        public static Expression MainMultiplicationHandler(Expression expr1, Expression expr2)
        {
            string Expr1Type = HelperMethods.ExpressionType(expr1).ToLower();
            string Expr2Type = HelperMethods.ExpressionType(expr2).ToLower();
            switch(Expr1Type)
            {
                case "constant":
                            Constant TempExpr1 = (Constant)expr1;
                                if (TempExpr1.Calculate() == 0)
                                {
                                    return Constant.GetConstant(0.0);
                                }
                                else if (TempExpr1.Calculate() == 1)
                                {
                                    return expr2;
                                }
                                else
                                {
                                    switch (Expr2Type)
                                    {

                                        case "constant":
                                            return ConstantTimesConstant((Constant)expr1, (Constant)expr2);
                                        case "cosine":
                                            return new Multiplication(expr1, expr2);
                                        case "naturallogarithm":
                                            return new Multiplication(expr1, expr2);
                                        case "negation":
                                            return new Multiplication(expr1, expr2);
                                        case "sine":
                                            return new Multiplication(expr1, expr2);
                                        case "variable":
                                            return new Multiplication(expr1, expr2);
                                        case "addition":
                                            return ConstantTimesAddition((Constant)expr1, (Addition)expr2);
                                        case "division":
                                            return ConstantTimesDivision((Constant)expr1, (Division)expr2);
                                        case "multiplication":
                                            return ConstantTimesMultiplication((Constant)expr1, (Multiplication)expr2);
                                        case "power":
                                            return ConstantTimesPower((Constant)expr1, (Power)expr2);
                                        case "substraction":
                                            return ConstantTimesSubstraction((Constant)expr1, (Substraction)expr2);
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
                                        return expr2 * expr1;
                                    case "variable":
                                        //return new Power(Variable.GetVariable, Constant.GetConstant(2));
                                        return VariableTimesVariable((Variable)expr1,(Variable)expr2);
                                    case "addition":
                                        return VariableTimesAddition((Variable)expr1, (Addition)expr2);
                                    case "division":
                                        return VariableTimesDivision((Variable)expr1, (Division)expr2);
                                    case "multiplication":
                                        return VariableTimesMultiplication((Variable)expr1, (Multiplication)expr2);
                                    case "power":
                                        return VariableTimesPower((Variable)expr1, (Power)expr2);
                                    case "substraction":
                                        return VariableTimesSubstraction((Variable)expr1, (Substraction)expr2);

                                }
                                break;
                case "addition":
                                switch (Expr2Type)
                                {
                                    case "constant":
                                        return ConstantTimesAddition((Constant)expr2, (Addition)expr1);
                                    case "variable":
                                        return VariableTimesAddition((Variable)expr2, (Addition)expr1);
                                    case "addition":
                                        return AdditionTimesAddition((Addition)expr1, (Addition)expr2);
                                }
                                break;
                case "multiplication":
                                switch (Expr2Type)
                                {
                                    case "constant":
                                        return ConstantTimesMultiplication((Constant)expr2, (Multiplication)expr1);
                                    case "power":
                                        return MultiplicationTimesPower((Multiplication)expr1, (Power)expr2);
                                    case "variable":
                                        return VariableTimesMultiplication((Variable)expr2, (Multiplication)expr1);
                                }
                                break;
                case "division":
                                switch (Expr2Type)
                                {
                                    case "constant":
                                        return ConstantTimesDivision((Constant)expr2, (Division)expr1);
                                    case "variable":
                                        return expr2 * expr1;
                                }
                                break;
                case "power":
                                switch (Expr2Type)
                                {
                                    case "constant":
                                        return expr2 * expr1;
                                    case "variable":
                                        return expr2 * expr1;
                                    case "multiplication":
                                        return expr2 * expr1;
                                    case "power":
                                        return PowerTimesPower((Power)expr1, (Power)expr2);
                                }
                                break;

            }
            return new Multiplication(expr1, expr2);
        }

        #region ConstantTimes
        private static Expression ConstantTimesConstant(Constant constant1, Constant constant2)
        {
            return Constant.GetConstant(constant1.Calculate() * constant2.Calculate());
        }
        private static Expression ConstantTimesAddition(Constant constant, Addition addition)
        {
            Expression addition1 = addition.ReturnFirstExpression();
            Expression addition2 = addition.ReturnSecondExpression();

            return constant * addition1 + constant * addition2;
        }
        private static Expression ConstantTimesDivision(Constant constant, Division division)
        {
            return (constant * division.ReturnFirstExpression()) / division.ReturnSecondExpression();
        }
        private static Expression ConstantTimesMultiplication(Constant constant, Multiplication multiplication)
        {
            return (constant * multiplication.ReturnFirstExpression()) * multiplication.ReturnSecondExpression();
        }
        private static Expression ConstantTimesPower(Constant constant, Power power)
        {
            if (power.ReturnFirstExpression() is Constant)
            {
                Constant TempPower1 = (Constant)power.ReturnFirstExpression();
                if (constant.Calculate() == TempPower1.Calculate())
                {
                    return new Power(power.ReturnFirstExpression(), power.ReturnSecondExpression() + Constant.GetConstant(1));
                }
            }
            return new Multiplication(constant, power);            
        }
        private static Expression ConstantTimesSubstraction(Constant constant, Substraction substraction)
        {
            Expression substraction1 = substraction.ReturnFirstExpression();
            Expression substraction2 = substraction.ReturnSecondExpression();
            return constant * substraction1 - constant * substraction2;
        }
        #endregion
        #region VariableTimes
        private static Expression VariableTimesVariable(Variable variable1, Variable variable2)
        {
            return new Power(Variable.GetVariable, Constant.GetConstant(2));
        }
        private static Expression VariableTimesAddition(Variable variable, Addition addition)
        {
            Expression addition1 = addition.ReturnFirstExpression();
            Expression addition2 = addition.ReturnSecondExpression();

            return variable * addition1 + variable * addition2;
        }
        private static Expression VariableTimesDivision(Variable variable, Division division)
        {
            return (variable * division.ReturnFirstExpression()) / division.ReturnSecondExpression();
        }
        private static Expression VariableTimesMultiplication(Variable variable, Multiplication multiplication)
        {
            Expression multiplication1 = multiplication.ReturnFirstExpression();
            Expression multiplication2 = multiplication.ReturnSecondExpression();
            if (multiplication1 is Variable)
            {
                return new Multiplication(variable*multiplication1, multiplication2);
            }
            else if (multiplication2 is Variable)
            {
                return new Multiplication(multiplication1,variable*multiplication2);
            }
            else if (multiplication2 is Power)
            {
                Power Multiplication2Temp = (Power)multiplication2;
                if (Multiplication2Temp.ReturnFirstExpression() is Variable)
                {
                     return new Multiplication(multiplication1,variable*multiplication2);
                }
            }
            return new Multiplication(variable*multiplication1,multiplication2);
        }
        private static Expression VariableTimesPower(Variable variable, Power power)
        {
            Expression power1 = power.ReturnFirstExpression();
            Expression power2 = power.ReturnSecondExpression();
            if (power1 is Variable)
            {
                return new Power(power1, power2 + Constant.GetConstant(1));
            }
            return new Multiplication(variable, power);

        }
        private static Expression VariableTimesSubstraction(Variable variable, Substraction substraction)
        {
            Expression substraction1 = substraction.ReturnFirstExpression();
            Expression substraction2 = substraction.ReturnSecondExpression();
            return variable * substraction1 - variable * substraction2;
        }
#endregion
        #region AdditionTimes
        private static Expression AdditionTimesAddition(Addition expr1, Addition expr2)
        {
            Expression FirstTimesFirst = expr1.ReturnFirstExpression().Multiply(expr2.ReturnFirstExpression());
            Expression FirstTimesSecond = expr1.ReturnFirstExpression().Multiply(expr2.ReturnSecondExpression());
            Expression SecondTimesFirst = expr1.ReturnSecondExpression().Multiply(expr2.ReturnFirstExpression());
            Expression SecondTimesSecond = expr1.ReturnSecondExpression().Multiply(expr2.ReturnSecondExpression());
            return FirstTimesFirst.Add(FirstTimesSecond.Add(SecondTimesFirst.Add(SecondTimesSecond)));
        }
        #endregion
        #region MultiplicationTimes
        private static Expression MultiplicationTimesPower(Multiplication multiplication, Power power)
        {
            Expression multiplication1 = multiplication.ReturnFirstExpression();
            Expression multiplication2 = multiplication.ReturnSecondExpression();

            if (multiplication2 is Power)
            {
                return multiplication1 * (multiplication2 * power);
            }
            return new Multiplication(multiplication, power);
        }
        #endregion
        #region PowerTimes
        private static Expression PowerTimesPower(Power power1, Power power2)
        {
            Expression power1Base = power1.ReturnFirstExpression();
            Expression power2Base = power2.ReturnFirstExpression();

            string power1BaseType = HelperMethods.ExpressionType(power1Base).ToLower();
            string power2BaseType = HelperMethods.ExpressionType(power2Base).ToLower();

            if (power1BaseType == power2BaseType)
            {
                switch (power1BaseType)
                {
                    case "constant":
                        if (((Constant)power1Base).Calculate() == ((Constant)power2Base).Calculate())
                        {
                            return new Power(power1Base, power1.ReturnSecondExpression() + power2.ReturnSecondExpression());
                        }
                        return new Multiplication(power1, power2);
                    case "variable":
                        return new Power(Variable.GetVariable, power1.ReturnSecondExpression() + power2.ReturnSecondExpression());
                }
            }
            return new Multiplication(power1, power2);            
        }
        #endregion

    }
}
