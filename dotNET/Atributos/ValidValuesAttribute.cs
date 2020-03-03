using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET.Atributos
{


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class ValidValuesAttribute : Attribute
    {

        public string Valor { get; set; }
        public string Descricao { get; set; }


        public ValidValuesAttribute(string valor, string descricao)
        {

            this.Valor = valor;
            this.Descricao = descricao;
            

        }


    }
}
