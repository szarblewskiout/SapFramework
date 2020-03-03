using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET.Atributos
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class UdoChildAttribute : Attribute
    {

        public string TableName { get; set; }
        public string TabelaPai { get; set; }


        public UdoChildAttribute(string tableName, string tabelaPai)
        {
            this.TableName = tableName;
            this.TabelaPai = tabelaPai;
        }


    }
}
