using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class TablesChildAttribute : Attribute
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomePai { get; set; }


        public TablesChildAttribute(string nome, string descricao, string nomePai)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.NomePai = nomePai;
        }


    }
}
