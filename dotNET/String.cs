using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET
{
    public class StringExtensions
    {

        public static string RetornaNumero(string valor)
        {
            return valor.ToCharArray().Where(c => Char.IsNumber(c)).Aggregate("", (current, c) => current + c);
        }
    }
}
