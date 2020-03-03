using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.SAP.UI.Utils
{
    internal class InternalUtils
    {

        /// <summary>
        /// formata data para input em tela
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Retorna formato yyyyMMdd </returns>
        public static string FormataData(DateTime data)
        {

            return data.ToString("yyyyMMdd");
        }



    }
}
