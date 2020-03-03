using SAPbouiCOM;
using SapFramework.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SapFramework.SAP.UI.Forms
{
    public static class FormsXml
    {

        public static Form AdicionaTela(string tela, string formType)
        {
            try
            {
                FormCreationParams creationPackage = (FormCreationParams)B1AppDomain.Application.CreateObject(BoCreatableObjectType.cot_FormCreationParams);
                XmlDocument document = new XmlDocument
                {
                    InnerXml = tela
                };
                creationPackage.XmlData = document.InnerXml;
                creationPackage.UniqueID = DateTime.Now.ToBinary().ToString();
                creationPackage.FormType = formType;
                return B1AppDomain.Application.Forms.AddEx(creationPackage);
            }
            catch (Exception exception)
            {
                B1Exception.throwException("Erro ao carregar formularios XML :: ", exception);
            }
            return null;
        }


    }
}
