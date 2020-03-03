using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAPbouiCOM;
using SapFramework.Connections;

namespace SapFramework.SAP.UI.Utils
{
    public static class Menu
    {

        /**
         * Adiciona um item de menu em um menu ja existente ou adicionado.
         * 
         * 
         * menuItemB1ID     Id do menu pai onde o item de menu devera ser inserido
         * menuItemDescr    Texto do item de menu
         * menuItemID       ID do item de menu que esta sendo criado
         * position         posicao na lista de itens dentro de um menu
         * type             tipo...
         * imagePath        caminho para o arquivo de imagem a ser utilizado como icone no menu
         * 
         */
        public static void AddMenuItem(string menuItemB1ID, string menuItemDescr, string menuItemID
            , int position, BoMenuType type, string imagePath = "", bool remove = true)
        {
            SAPbouiCOM.Menus oMenus = null;
            SAPbouiCOM.MenuItem oMenuItem = null;
            SAPbouiCOM.MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(B1AppDomain.Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

            oMenuItem = B1AppDomain.Application.Menus.Item(menuItemB1ID);
            oMenus = oMenuItem.SubMenus;

            bool exist = (oMenus != null) && oMenuItem.SubMenus.Exists(menuItemID);

            if (exist && remove)
            {
                oMenuItem.SubMenus.RemoveEx(menuItemID);
                exist = false;
            }
            else
            {
                exist = false;
            }


            if (!(exist && remove))
            {
                oCreationPackage.Type = type;
                oCreationPackage.UniqueID = menuItemID;
                oCreationPackage.String = menuItemDescr;
                oCreationPackage.Enabled = true;
                oCreationPackage.Position = position; //posição onde vai criar o modulo
                oCreationPackage.Image = imagePath;

                try
                {
                    if (oMenus == null)
                    {
                        oMenuItem.SubMenus.Add(menuItemID, menuItemDescr, type, position);
                        oMenus = oMenuItem.SubMenus;
                    }

                    oMenus.AddEx(oCreationPackage);
                }
                catch (Exception ex)
                {
                    // B1Exception.throwException("Erro ao criar Menu ::", ex);
                }
            }

        }


        public static void AddMenuItemContext(string uniqueId, string descricao, int posicao)
        {
            SAPbouiCOM.MenuItem oMenuItem = null;
            SAPbouiCOM.Menus oMenus = null;


            try
            {
                SAPbouiCOM.MenuCreationParams oCreationPackage = null;

                oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(B1AppDomain.Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = uniqueId;
                oCreationPackage.String = descricao;
                oCreationPackage.Enabled = true;
                oCreationPackage.Position = posicao;
                
                oMenuItem = B1AppDomain.Application.Menus.Item("1280"); // Data'
                oMenus = oMenuItem.SubMenus;
                oMenus.AddEx(oCreationPackage);

            }
            catch 
            {
                
            }
        }
    }
}
