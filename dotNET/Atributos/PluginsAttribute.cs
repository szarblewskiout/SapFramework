using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET.Atributos
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class PluginsAttribute : Attribute
    {

        public string Nome;
        public string Descricao;
        public string Versao;

        public PluginsAttribute(string nome, string descricao, string versao)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Versao = versao;
        }

    }
}
