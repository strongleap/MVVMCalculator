using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalculator
{
    public class Divide : IOperator
    {
        public int Evaluate(int leftHand, int rightHand)
        {
            return leftHand / rightHand;
        }

        public override string ToString()
        {
            return "/";
        }
    }
}
