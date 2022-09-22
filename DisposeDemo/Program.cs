using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisposeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DispClass d = new DispClass();
            d.Dispose();
            GC.Collect();
            Console.Read();
            //GC.Collect();
        }
    }
}
