using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoInterface;

namespace DemoDAL
{
    class CalcAdd : ICalc
    {

        public double CalacMums(double calcNumA, double calcNumB)
        {
            return calcNumA + calcNumB ;
        }
    }
}
