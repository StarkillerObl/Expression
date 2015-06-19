using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionClassLibrary.Expressions
{
    public class Variable : Expression
    {
        public Variable(string VariableName)
        {
            this.Name = VariableName;
            if (!Names.Contains(VariableName))
            {
                Names.Add(VariableName);
                Names.Sort();
                Values.Add(new Valued(VariableName));
                Values = Values.OrderBy(x => x.Name).ToList();
            }
        }

        //private static Variable _var;
        public string Name { get; private set; }
        public static List<string> Names { get; private set; }

        public static List<Valued> Values { get; private set; }

        public static void AddValue(string VariableName, double VariableValue)
        {
            foreach (var item in Values)
            {
                if (item.Name == VariableName)
                {
                    item.Value = VariableValue;
                }
            }
        }

        public static Variable GetVariable
        {
            //get { return _var ?? (_var = new Variable()); }
            //return 

        }

        public override Expression Derivative(string VariableName)
        {
            if (this.Name == VariableName)
            {
                return Constant.GetConstant(1);
            }
            else
                return Constant.GetConstant(0);

        }

        public override double Calculate(double? point = null)
        {
            if (point == null)
                throw new ArgumentNullException("Wartość zmiennej nie może być pusta!");

            return (double)point;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    private class Valued
    {
        public string Name { get; set; }
        public double? Value { get; set; }

        public Valued(string VariableName)
        {
            this.Name = VariableName;
            this.Value = null;
        }
    }
}
