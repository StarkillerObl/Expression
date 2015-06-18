using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionClassLibrary.BinaryExpressions;
using ExpressionClassLibrary.MathematicalOperationHandlers;

namespace ExpressionClassLibrary.Expressions
{
    public abstract class Expression
    {

        public virtual Expression Add(Expression expr)
        {
            //return new Addition(this,expr);
            //return Addition.AdditionHandler(this, expr);
            return AdditionHandlers.MainAdditionHandler(this, expr);
        }
        public virtual Expression Substract(Expression expr)
        {
            return new Substraction(this,expr);
        }

        public virtual Expression Multiply(Expression expr)
        {
            //return new Multiplication(this,expr);
            //return Multiplication.MultiplicationHandler(this, expr);
            return MultiplicationHandlers.MainMultiplicationHandler(this, expr);
        }
        public virtual Expression Divide(Expression expr)
        {
            //return new Division(this, expr);
            return DivisionHandlers.MainDivisionHandler(this, expr);
        }
        public virtual Expression Power(Expression expr)
        {
            return new Power(this, expr);
        }
        public virtual Expression Negate()
        {
            return new Negation(this);
        }
        public virtual Expression Ln()
        {
            return new NaturalLogarithm(this);
        }
        public virtual Expression Sine()
        {
            return new Sine(this);
        }
        public virtual Expression Cosine()
        {
            return new Cosine(this);
        }
        public static Expression operator +(Expression expr1, Expression expr2)
        {
            //return new Addition(expr1, expr2);
            return expr1.Add(expr2);
        }
        public static Expression operator -(Expression expr1, Expression expr2)
        {
            return new Substraction(expr1, expr2);
        }
        public static Expression operator *(Expression expr1, Expression expr2)
        {
            //return new Multiplication(expr1, expr2);
            return expr1.Multiply(expr2);
        }
        public static Expression operator /(Expression expr1, Expression expr2)
        {
            //return new Division(expr1, expr2);
            return expr1.Divide(expr2);
        }
        public static Expression operator !(Expression expr1)
        {
            return expr1.Negate();
        }
        public double Integral(double from, double to)
        {
            //dokładność
            var step = (to - from) / 100d;
            var result = 0d;

            double i = from;
            while (i < to)
            {
                result += Calculate(from + i * step) * step;
                i += step;
            }
            return result;
        }
        public static bool ExpressionEqualsExpression(Expression expr1, Expression expr2)
        {
            if (expr1.ToString() == expr2.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public abstract Expression Derivative();
        public abstract double Calculate(double? point = null);
        public abstract override string ToString();

        public static List<string> InfixToPostfix(List<string> input)
        {
            Stack<Operator> operators = new Stack<Operator>();
            List<string> output = new List<string>();

            for (int i = 0; i < input.Count; i++)
            {
                if (Operator.IsAnOperator(input[i]))
                {
                    
                    Operator temp = new Operator(input[i]);
                    if (input[i] == "(")
                    {
                        if (i > 0 && !Operator.IsAnFunctionalOperator(input[i - 1]) && input[i - 1] != "(")
                        {
                            operators.Push(new Operator("*"));
                        }
                        operators.Push(temp);
                    }
                    else if (input[i] == ")")
                    {
                        while (operators.Peek().ToString() != "(")
                        {
                            output.Add(operators.Pop().ToString());
                        }
                        operators.Pop();
                    }
                    else if (operators.Count == 0 || temp > operators.Peek())
                    {
                        if (temp.ToString() == "-" && (i == 0 || input[i - 1] == "("))
                        {
                            operators.Push(new Operator("NEG"));
                        }
                        else
                        {
                            operators.Push(temp);
                        }                                     
                    }
                    else
                    {
                        if (temp.ToString() == "-" && i > 0 && Operator.IsAnFunctionalOperator(input[i - 1]))
                        {
                            operators.Push(new Operator("NEG"));
                        }
                        else
                        {
                            while (operators.Count != 0 && operators.Peek() >= temp)
                            {
                                output.Add(operators.Pop().ToString());
                            }
                            operators.Push(temp);
                        }
                        
                    }
                }
                else
                {
                    output.Add(input[i]);
                }
            }
            if (operators.Count != 0)
            {
                while (operators.Count!=0) 
                {
                    output.Add(operators.Pop().ToString());
                }
            }
            return output;
        }
        public static Expression PostfixToExpression(List<string> input)
        {
            Stack<Expression> stack = new Stack<Expression>();
            for (int i = 0; i < input.Count; i++)
            {
                string temp = input[i];
                if (!Operator.IsAnOperator(input[i]))
                {
                    if (temp == "x")
                    {
                        stack.Push(Variable.GetVariable);
                    }
                    else
                    {
                        stack.Push(Constant.GetConstant(Convert.ToDouble(temp)));
                    }
                }
                else
                {
                    Expression expr1 = stack.Pop();
                    temp = temp.ToLower();
                    switch (temp)
                    {
                        case "+":
                            {
                                Expression expr2 = stack.Pop();
                                stack.Push(expr2 + expr1);
                                break;
                            }                            
                        case "-":
                            {
                                Expression expr2 = stack.Pop();
                                stack.Push(expr2 - expr1);
                                break;
                            }                            
                        case "*":
                            {
                                Expression expr2 = stack.Pop();
                                stack.Push(expr2 * expr1);
                                break;
                            }                            
                        case "/":
                            {
                                Expression expr2 = stack.Pop();
                                stack.Push(expr2 / expr1);
                                break;
                            }  
                        case "^":
                            {
                                Expression expr2 = stack.Pop();
                                stack.Push(expr2.Power(expr1));
                                break;
                            }  
                        case "neg":
                            //stack.Push(!expr1);
                            stack.Push(expr1.Negate());
                            break;
                        case "ln":
                            stack.Push(expr1.Ln());
                            break;
                        case "sin":
                            stack.Push(expr1.Sine());
                            break;
                        case "cos":
                            stack.Push(expr1.Cosine());
                            break;
                    }
                }
            }
            return stack.Pop();
        }
        public static Expression StringToExpression(string input)
        {
            List<string> Infix = new List<string>();
            List<string> Postfix = new List<string>();

            Infix = HelperMethods.InputToList(input);
            Postfix = InfixToPostfix(Infix);

            return PostfixToExpression(Postfix);
        }
    }
}
 