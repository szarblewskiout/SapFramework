using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.dotNET.Atributos
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class EventsAttribute : Attribute
    {

        public string ItemUid;
        public string ColUid;
        public int RowUid;

        public EventsAttribute(string itemuid, string coluid, int rowuid)
        {
            this.ItemUid = itemuid;
            this.ColUid = coluid;
            this.RowUid = rowuid;
        }


    }
}
