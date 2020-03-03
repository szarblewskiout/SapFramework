using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SapFramework.BaseDados.enumeradores;

namespace SapFramework.BaseDados
{
    public class Tabelas : ITabelas
    {

        #region Propriedades

        public string NomeTabela { get; set; }
        public string DescricaoTabela { get; set; }
        public SAPbobsCOM.BoUTBTableType TipoTabelaSap { get; set; }
        public Tipos.TipoTabela Ttabela { get; set; }
        public BD.OperacoesBd Operacao { get; set; }
        public Udo Udos { get; set; }
        

        #endregion

        public List<Tabelas> TabelasFilhas { get; set; } 

        public List<Campos> Campos{ set; get; }
        
        public string XmlTela { get; set; }


    }
}
