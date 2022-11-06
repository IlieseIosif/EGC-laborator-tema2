using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iosif_3133A_tema2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Paralelipiped paralelipiped = new Paralelipiped())
            {
                paralelipiped.Run(30, 0);
            }
        }
    }
}
