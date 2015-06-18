using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ExpressionClassLibrary.Expressions
{
    public class Constant: Expression
    {
        private double _value;
 
        protected Constant(double value)
        {
            _value = value;
        }
 
        public static Constant GetConstant(double value)
        {
            return new Constant(value);
        }
 
        public override Expression Derivative()
        {
            return GetConstant(0);
        }
 
        public override double Calculate(double? point = null)
        {
            return _value;
        }
 
        public override string ToString()
        {
            return String.Format("{0:0.00}", _value);
        }
    }
}