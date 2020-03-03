using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;

namespace SapFramework.BaseDados
{
    public class Udo : IUdo
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
        public string TabelaPai { get; set; }
        public string Formulario { get; set; }
        public BD.OperacoesBd Operacao { get; set; }
        public List<UdoFilhos> Filhos { get; set; }

        public List<Udo> ListaUdos { get; set; }


        public SAPbobsCOM.BoYesNoEnum EnableEnhancedform { get; set; }

        public BoYesNoEnum RebuildEnhancedForm { get; set; }

    }


    public class UdoFilhos
    {
        public string TableName { get; set; }
        public string TabelaPai { get; set; }
    }
}
