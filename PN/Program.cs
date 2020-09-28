using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN
{
    class Program
    {
        static void Main(string[] args)
        {
            Transform transform = new Transform();
            string expression = "3+4*2/(1-5)^2";

            transform.Start(expression);
        }
    }
}
