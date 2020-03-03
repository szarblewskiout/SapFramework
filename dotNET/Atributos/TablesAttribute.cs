using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using SapFramework.BaseDados;
using SAPbobsCOM;

namespace SapFramework.dotNET.Atributos
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TablesAttribute : Attribute
    {

        
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public BoUTBTableType Tipo { get; set; }
        public bool TabelaSistema { get; set; }
        


        public TablesAttribute(string nome, string descricao, BoUTBTableType tipo, bool tabelaSistema)
        {

            this.Tipo = tipo;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TabelaSistema = tabelaSistema;




        }

        public TablesAttribute(string nome, string descricao, bool tabelaSistema)
        {

            
            this.Nome = nome;
            this.Descricao = descricao;
            this.TabelaSistema = tabelaSistema;


        }

        

    }
}
