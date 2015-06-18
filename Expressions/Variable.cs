using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionClassLibrary.Expressions
{
    public class Variable : Expression
    {
        private Variable()
        {
        }

        private static Variable _var;
        public static Variable GetVariable
        {
            get { return _var ?? (_var = new Variable()); }
        }

        public override Expression Derivative()
        {
            return Constant.GetConstant(1);
        }

        public override double Calculate(double? point = null)
        {
            if (point == null)
                throw new ArgumentNullException("Wartość zmiennej nie może być pusta!");

            return (double)point;
        }

        public override string ToString()
        {
            return "x";
        }
    }
}
