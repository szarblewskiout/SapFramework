using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET.Atributos
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class MenuAttribute : Attribute
    {

        public string menuUid;
        public string descricao;

        public MenuAttribute(string menuUid, string descricao)
        {
            this.menuUid = menuUid;
            this.descricao = descricao;
        }

    }
}
