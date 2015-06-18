using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionClassLibrary.Expressions;

namespace ExpressionClassLibrary
{
    public class HelperMethods
    {
        public static List<string> InputToList(string input)
        {
            input = RemoveWhiteSigns(input);
            List<string> Result = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                string temp = "";
                if (IsNumeric(input[i]))
                {
                    while (i<input.Length && (IsNumeric(input[i]) || input[i] == ','))
                    {
                        temp += input[i];
                        i++;
                    }

                    Result.Add(temp);
                    temp = "";
                    if(i>= input.Length)
                    {
                        break;
                    }
                   
                }

                if (i < input.Length && (input[i] == 'x' || input[i] == 'X'))
                {
                    if (Result.Count > 0 && !Operator.IsAnFunctionalOperator(Result[Result.Count - 1]) && Result[Result.Count - 1] != "(")
                    {
                        Result.Add("*");
                    }
                    Result.Add("x");
                }
                //else if (i < input.Length && (Operator.IsAnOperator(Convert.ToString(input[i]))))
                //{
                //    Result.Add(Convert.ToString(input[i]));
                //}
                else
                {
                    temp += input[i];
                    while(!Operator.IsAnOperator(temp))
                    {
                        i++;
                        temp+= input[i];                        
                    }
                    Result.Add(temp);
                    temp = "";
                    
                }
            }
            return Result;
        }
        public static bool IsNumeric(char sign)
        {
            if (sign == '0' || sign == '1' || sign == '2' || sign == '3' || sign == '4' || sign == '5' || sign == '6' || sign == '7' || sign == '8' || sign == '9' || sign == ',')
            {
                return true;
            }
            else
                return false;
        }
        public static string RemoveWhiteSigns(string input)
        {
            string Result = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != ' ')
                {
                    Result += input[i];
                }
            }
            return Result;
        }

        public static string ExpressionType(Expression expression)
        {
            //string FullExpressionType = expression.GetType().ToString();
            //string Result = "";
            //int i = FullExpressionType.Length - 1;
            //while (FullExpressionType[i] != '.')
            //{
            //    Result = FullExpressionType[i] + Result;
            //    i--;
            //}
            string Result = expression.GetType().Name.ToString();
            return Result;
        }
       
    }
}
