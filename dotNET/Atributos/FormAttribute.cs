using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET.Atributos
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FormAttribute : Attribute
    {

        public string formUid;
        public string descricao;

        public FormAttribute(string formUid, string descricao)
        {
            this.formUid = formUid;
            this.descricao = descricao;
        }

    }
}
