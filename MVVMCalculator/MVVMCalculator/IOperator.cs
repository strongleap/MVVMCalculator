using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalculator
{
    public interface IOperator
    {
        int Evaluate(int leftHand, int rightHand);

        string ToString();
    }
}
