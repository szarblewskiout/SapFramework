using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SapFramework.BaseDados.enumeradores;


namespace SapFramework.BaseDados
{
    public interface ITabelas
    {

        string NomeTabela { get; set; }
        string DescricaoTabela { get; set; }
        SAPbobsCOM.BoUTBTableType TipoTabelaSap { get; set; }
        Tipos.TipoTabela Ttabela { get; set; }
        List<Campos> Campos { get; set; }
        string XmlTela { get; set; }
        BD.OperacoesBd Operacao { get; set; }
        Udo Udos { get; set; }
        List<Tabelas> TabelasFilhas { get; set; } 
        
    }
}
