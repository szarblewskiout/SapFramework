using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SapFramework.Models;
using SapFramework.SAP.Events;
using System.Reflection;
using System.Windows.Forms;
using SapFramework.BaseDados;
using SapFramework.dotNET.Atributos;
using SAPbobsCOM;
using SAPbouiCOM;

namespace SapFramework.Connections.UI
{
    public sealed class ConnectUI
    {

        private bool b1Connected = false;

        public bool Connected
        {
            get
            {
                return b1Connected;
            }
        }

        public void Connect(Type[] types)
        {
            SAPbouiCOM.SboGuiApi objGUIApi = null;
            SAPbouiCOM.Application objApplication = null;
            SAPbobsCOM.Company objCompany = null;
            B1AppDomain.ConnectionString = (string)Environment.GetCommandLineArgs().GetValue(1);
            //"0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";
            //"0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056"; 
            try
            {
                objGUIApi = new SAPbouiCOM.SboGuiApi();
                objGUIApi.Connect(B1AppDomain.ConnectionString);
                objApplication = objGUIApi.GetApplication(-1);
                objCompany = (SAPbobsCOM.Company)objApplication.Company.GetDICompany();

                b1Connected = objCompany.Connected;
                if (b1Connected)
                {
                    B1AppDomain.Application = objApplication;
                    B1AppDomain.Company = objCompany;
                    B1AppDomain.Connected = true;
                    B1AppDomain.Application.SetStatusBarMessage("Conexão estabelecida com sucesso!", BoMessageTime.bmt_Short, false );
                    new Events();
                    B1AppDomain.Application.SetStatusBarMessage("Carregando Aplicação...", BoMessageTime.bmt_Short, false);
                    CriaInstanciaClasses(types);
                    B1AppDomain.Application.SetStatusBarMessage("Carregamento Concluido!", BoMessageTime.bmt_Short, false);

                }
                else
                {
                    B1AppDomain.Connected = false;
                }

            }
            catch (Exception er)
            {
                B1AppDomain.Connected = false;
                B1Exception.throwException("Erro ao conectar no SAP B1: ", er);

            }

            
        }
        
        public static Plugins GetDadosPlugins(string path)
        {
            PluginsAttribute attribute = null;
            Assembly asm = Assembly.LoadFrom(path);

            var plg = new Plugins();

            foreach (Type type in asm.GetTypes())
            {
                foreach (object obj in type.GetCustomAttributes(false))
                {
                    if (obj is PluginsAttribute)
                    {
                        attribute = obj as PluginsAttribute;

                        if (!string.IsNullOrEmpty(attribute.Nome))
                        {

                            plg.Nome = attribute.Nome;
                            plg.Descrição = attribute.Descricao;
                            plg.Versao = attribute.Versao;
                            break;

                        }



                    }

                }
            }



           
            return plg;

        }
        
        public static void CriaInstanciaPlugins(string path)
        {
            Assembly asm = Assembly.LoadFrom(path);

            Type[] types = asm.GetTypes();

            foreach (Type type in types)
            {
                if (type.Name != "Program")
                {
                    try
                    {
                        Activator.CreateInstance(type);
                    }
                    catch { }


                }

            }


        }

        static void CriaInstanciaClasses(Type[] nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            
            foreach (Type type in nameSpace)
            {
                if (type.Name != "Program")
                {
                    try
                    {
                        Activator.CreateInstance(type);    
                    }
                    catch { }
                    
                        
                }
                
            }

        }
       
    }
}
