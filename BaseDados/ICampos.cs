using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;

namespace SapFramework.BaseDados
{
    public interface ICampos
    {

        string NomeTabela { set; }
        string NomeCampo { set; }
        string DescricaoCampo { set; }
        BoFieldTypes TipoCampo { set; }
        BoFldSubTypes SubTipos { set; }
        BoYesNoEnum Mandatory { set; }
        string ValorPadrao { set; }
        int Tamanho { set; }
        BD.OperacoesBd Operacao { set; }
        List<ValoresValidos> ValoresValidos { get; set; }
        string UdoReferencia { get; set; }
        string TabelaReferencia { get; set; }

        void Add();
    }
}
