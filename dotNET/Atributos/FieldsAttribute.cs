using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SapFramework.BaseDados;
using SAPbobsCOM;

namespace SapFramework.dotNET.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FieldsAttribute : Attribute
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Tamanho { get; set; }
        public string ValorPadrao { get; set; }
        public bool Obrigatorio { get; set; }
        public BoFldSubTypes SubTipo { get; set; }
        public string LinkUdo { get; set; }

        
        /// <summary>
        /// Define Informações para os campos a serem criados na base sap
        /// </summary>
        /// <param name="nome">Nome do Campo na base SAP</param>
        /// <param name="descricao">Descrição do campo na base SAP</param>
        /// <param name="tamanho">Tamanho para o campo</param>
        /// <param name="obrigatorio">Define se o preenchimento do campo é obrigatorio</param>
        /// <param name="subtipo">Define o subTipo do Campo para o sistema SAP</param>
        public FieldsAttribute(string nome, string descricao, int tamanho, bool obrigatorio, BoFldSubTypes subtipo)
        {

            this.Nome = nome;
            this.Descricao = descricao;
            this.Tamanho = tamanho;
            this.Obrigatorio = obrigatorio;
            this.SubTipo = subtipo;
            

        }

        public FieldsAttribute(string nome, string descricao, int tamanho, bool obrigatorio, BoFldSubTypes subtipo, string linkUdo)
        {

            this.Nome = nome;
            this.Descricao = descricao;
            this.Tamanho = tamanho;
            this.Obrigatorio = obrigatorio;
            this.SubTipo = subtipo;
            this.LinkUdo = linkUdo;


        }

        public FieldsAttribute(string nome, string descricao, int tamanho, string valorPadrao, bool obrigatorio, BoFldSubTypes subtipo )
        {

            this.Nome = nome;
            this.Descricao = descricao;
            this.Tamanho = tamanho;
            this.ValorPadrao = valorPadrao;
            this.Obrigatorio = obrigatorio;
            this.SubTipo = subtipo;



        }



    }
}
