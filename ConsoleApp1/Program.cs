using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
           
            string name = args[0];
            //spp = Convert.ToInt16(args[1]);

            //FileStream fs = new FileStream(name,FileMode.Open);
            PBRT_r re = new PBRT_r(name);
            re.ReadResolution();
            //PBRT pbrt = new PBRT(name, "32768");
        }
    }
}
