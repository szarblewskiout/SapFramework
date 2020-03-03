using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;

namespace SapFramework.BaseDados
{
    public class Campos : ICampos
    {


        #region Propriedades

        public string NomeTabela { get; set; }
        public string NomeCampo { get; set; }
        public string DescricaoCampo { get; set; }
        public BoFieldTypes TipoCampo { get; set; }
        public BoFldSubTypes SubTipos { get; set; }
        public BoYesNoEnum Mandatory { get; set; }
        public string ValorPadrao { get; set; }
        public int Tamanho { get; set; }
        public BD.OperacoesBd Operacao { get; set; }
        public string UdoReferencia { get; set; }
        public string TabelaReferencia { get; set; }

        #endregion

        public List<ValoresValidos> ValoresValidos { get; set; }


        public void Add()
        {
            new Campos();
        }


    }
}
