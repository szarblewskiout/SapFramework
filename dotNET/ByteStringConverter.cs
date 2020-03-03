using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET
{
    public class ByteStringConverter
    {


        public static string ConverterObjectToString(string caminho)
        {
            FileStream stream = File.OpenRead(caminho);
            byte[] fileBytes = new byte[stream.Length];
            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Close();
            return Convert.ToBase64String(fileBytes);


        }

        public static byte[] ConverterStringToByte(string arquivo)
        {
            return Convert.FromBase64String(arquivo);
        }


       


    }
}
