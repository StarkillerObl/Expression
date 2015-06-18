using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpressionClassLibrary
{
    public class Operator
    {
        private string _type;
        private int _priority;

        public Operator(string input)
        {
            this._type = input;
            SetPriority(input);
        }
        public Operator(char input)
        {
             new Operator(Convert.ToString(input));
        }
        #region Operators
        public override bool Equals(object obj)
        {
            if (obj is Operator)
            {
                Operator c = (Operator)obj;
                return c._priority == this._priority;
            }
            else
            { 
                return false; 
            }
        }
        public static bool operator ==(Operator x, Operator y)
        {
            return x._priority == y._priority;
        }
        public static bool operator !=(Operator x, Operator y)
        {
            return (x._priority != y._priority);
        }
        public static bool operator <(Operator x, Operator y)
        {
            return x._priority < y._priority;
        }
        public static bool operator >(Operator x, Operator y)
        {
            return x._priority > y._priority;
        }
        public static bool operator <=(Operator x, Operator y)
        {
            return x._priority <= y._priority;
        }
        public static bool operator >=(Operator x, Operator y)
        {
            return x._priority >= y._priority;
        }
        public override int GetHashCode()
        {
            return this._priority;
        }
        #endregion
        public override string ToString() 
        {
            return this._type;
        }
        public char ToChar()
        {
            if (this._type.Length < 2)
                return Convert.ToChar(this._type);
            else return ' ';
        }
        public int GetPriority()
        {
            return this._priority;
        }
        private void SetPriority(string input)
        {
            this._priority = OperatorPriority(input);
        }
        public static int OperatorPriority(string input)
        {
            input = input.ToLower();
            int priority = 100;
            switch (input)
            {
                case ")":
                case "(":
                    priority = 0;
                    break;
                case "+":
                case "-":
                    priority = 1;
                    break;
                case "*":
                case "/":
                    priority = 2;
                    break;
                case "sin":
                case "cos":
                case "neg":
                case "ln":
                case "^":
                    priority = 3;
                    break;
            }
            return priority;
        }
        public static bool IsAnOperator(string input)
        {
            if (OperatorPriority(input) == 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsAnFunctionalOperator(string input)
        {
            if (OperatorPriority(input) == 100 || OperatorPriority(input) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
