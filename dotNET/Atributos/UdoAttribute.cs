using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SapFramework.BaseDados;
using SAPbobsCOM;

namespace SapFramework.dotNET.Atributos
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class UdoAttribute : Attribute
    {

        public string TableName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public SAPbobsCOM.BoYesNoEnum Cancel { get; set; }
        public SAPbobsCOM.BoYesNoEnum Close { get; set; }
        public SAPbobsCOM.BoYesNoEnum CreateDefaultForm { get; set; }
        public SAPbobsCOM.BoYesNoEnum Delete { get; set; }
        public SAPbobsCOM.BoYesNoEnum Find { get; set; }
        public SAPbobsCOM.BoYesNoEnum YearTransfer { get; set; }
        public SAPbobsCOM.BoYesNoEnum ManageSeries { get; set; }
        public SAPbobsCOM.BoUDOObjType ObjectType { get; set; }
        public string Formulario { get; set; }
        public SAPbobsCOM.BoYesNoEnum EnableEnhancedform { get; set; }
        public BoYesNoEnum RebuildEnhancedForm { get; set; }


        public List<UdoFilhos> Filhos { get; set; }


        public UdoAttribute(string tableName, string name, string code, BoYesNoEnum cancel, BoYesNoEnum close,
            BoYesNoEnum createDefaultForm,
            BoYesNoEnum delete, BoYesNoEnum find, BoYesNoEnum yearTransfer, BoYesNoEnum manageSeries,
            BoUDOObjType objectType, string formulario,
            BoYesNoEnum enableEnhancedform, BoYesNoEnum rebuildEnhancedForm)
        {


            this.TableName = tableName;
            this.Name = name;
            this.Code = code;
            this.Cancel = cancel;
            this.Close = close;
            this.CreateDefaultForm = createDefaultForm;
            this.Delete = delete;
            this.Find = find;
            this.YearTransfer = yearTransfer;
            this.ManageSeries = manageSeries;
            this.ObjectType = objectType;
            this.Formulario = formulario;
            this.EnableEnhancedform = enableEnhancedform;
            this.RebuildEnhancedForm = rebuildEnhancedForm;


        }
        



    }
}
