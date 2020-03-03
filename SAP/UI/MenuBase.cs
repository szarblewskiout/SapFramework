using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbouiCOM;
using SapFramework.Connections;
using SapFramework.dotNET.Atributos;
using SapFramework.SAP.UI.Utils;

namespace SapFramework.SAP.UI
{
    public abstract class MenuBase
    {

        private readonly List<string> menuUid;


        protected MenuBase()
        {
            this.menuUid = new List<string>();
            MenuAttribute attribute = null;
            int index = 0;
            foreach (object obj2 in base.GetType().GetCustomAttributes(false))
            {
                if (obj2 is MenuAttribute)
                {
                    attribute = obj2 as MenuAttribute;

                    

                    this.menuUid.Add(attribute.menuUid);

                    B1AppDomain.RegisterMenuByType(this.menuUid.SingleOrDefault(e => e == attribute.menuUid), this);

                    index++;
                }
            }
            if (attribute == null)
            {
                B1Exception.writeLog("Falha ao indexar Form. Por favor checar os atributos informados");
            }
            
            
            this.OnInitializeFormEvents();


        }

        private void OnInitializeFormEvents()
        {
            
            #region Menu Listeners
            Global.MenuClick_Before += new Global.MenuEventHandler(MenuClickFilterBefore);
            Global.MenuClick_After += new Global.MenuEventHandler(MenuClickFilterAfter);
            #endregion

        }


        #region MenuClick Actions

        private void MenuClickFilterAfter(ref MenuEvent pVal, ref bool bubbleEvent)
        {
            if (this.menuUid.Contains(pVal.MenuUID))
            {
                MenuClick_After(ref pVal, ref bubbleEvent);
            }
        }
        private void MenuClickFilterBefore(ref MenuEvent pVal, ref bool bubbleEvent)
        {
            if (this.menuUid.Contains(pVal.MenuUID))
            {
                MenuClick_Before(ref pVal, ref bubbleEvent);
            }
        }
        public virtual void MenuClick_After(ref MenuEvent pVal, ref bool bubbleEvent) { }
        public virtual void MenuClick_Before(ref MenuEvent pVal, ref bool bubbleEvent) { }
        #endregion

       



    }
}
