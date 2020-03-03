using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework
{

    public class Objects
    {

        public static void LimpaMemoria(object obj)
        {

            Marshal.FinalReleaseComObject(obj);
            obj = null;
            GC.Collect();


        }

    }


 
}
