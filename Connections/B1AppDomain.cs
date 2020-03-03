using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SapFramework.BaseDados;
using SAPbobsCOM;
using SAPbouiCOM;
using SapFramework.SAP.UI;

namespace SapFramework.Connections
{
    public sealed class B1AppDomain
    {
        private EventForm eventForm;
        static private B1AppDomain objAppDomainClass = null;
        static private SAPbouiCOM.Application objApplication = null;
        static private SAPbobsCOM.Company objCompany = null;
        static private string strConnectionString = "";
        static public bool Connected { get; set; }

        public static Dictionary<string, FormBase> DicionarioFormEvent = new Dictionary<string, FormBase>();
        public static Dictionary<string, MenuBase> DicionarioMenuEvent = new Dictionary<string, MenuBase>();
        public static Dictionary<object, Tabelas> DicionarioTabelasCampos = new Dictionary<object, Tabelas>();
        public static Dictionary<object, Udo> DicionarioUdos = new Dictionary<object, Udo>();
        public static Dictionary<object, UdoFilhos> DicionarioUdosFilhos = new Dictionary<object, UdoFilhos>();



        static public string ConnectionString
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                return strConnectionString;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                strConnectionString = value;
                AppDomain.CurrentDomain.SetData(Settings.ConfigApplication.Default.SAPConnectionString, strConnectionString);

            }
        }

        static public SAPbobsCOM.Company Company
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                return objCompany;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                objCompany = value;
                AppDomain.CurrentDomain.SetData(Settings.ConfigApplication.Default.SAPCompany, objCompany);

            }
        }

        static public SAPbouiCOM.Application Application
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                return objApplication;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                objApplication = value;
                AppDomain.CurrentDomain.SetData(Settings.ConfigApplication.Default.SAPApplication, objApplication);
            }
        }


        internal static void RegisterUdoChild(object obj, UdoFilhos udo)
        {
            if (obj != null)
            {
                DicionarioUdosFilhos.Add(obj, udo);
            }
        }

        internal static void RegisterUdo(object obj, Udo udo)
        {
            if (obj != null)
            {
                DicionarioUdos.Add(obj, udo);
            }
        }

        internal static void RegisterTable(object obj, Tabelas tables)
        {
            if (obj != null)
            {
                DicionarioTabelasCampos.Add(obj, tables);
            }
        }


        internal static void RegisterFormByType(string formUid, FormBase formBase)
        {

            if (!string.IsNullOrEmpty(formUid))
            {
                DicionarioFormEvent.Add(formUid, formBase);
            }
            
        }

        internal static void RegisterMenuByType(string menuUid, MenuBase menuBase)
        {
            if (!string.IsNullOrEmpty(menuUid))
            {
                DicionarioMenuEvent.Add(menuUid, menuBase);
            }
        }

        public static Recordset RSQuery(string query)
        {

            Recordset rs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            rs.DoQuery(query);

            return rs;
        }
    }


}
