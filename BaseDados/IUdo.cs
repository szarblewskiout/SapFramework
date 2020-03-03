using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapFramework.BaseDados
{
    public interface IUdo
    {

        string TableName { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        SAPbobsCOM.BoYesNoEnum Cancel { get; set; }
        SAPbobsCOM.BoYesNoEnum Close { get; set; }
        SAPbobsCOM.BoYesNoEnum CreateDefaultForm { get; set; }
        SAPbobsCOM.BoYesNoEnum Delete { get; set; }
        SAPbobsCOM.BoYesNoEnum Find { get; set; }
        SAPbobsCOM.BoYesNoEnum YearTransfer { get; set; }
        SAPbobsCOM.BoYesNoEnum ManageSeries { get; set; }
        SAPbobsCOM.BoUDOObjType ObjectType { get; set; }
        string TabelaPai { get; set; }
        SAPbobsCOM.BoYesNoEnum EnableEnhancedform { get; set; }
        string Formulario { get; set; }
        BD.OperacoesBd Operacao { get; set; }
        SAPbobsCOM.BoYesNoEnum RebuildEnhancedForm { get; set; }


    }
}
