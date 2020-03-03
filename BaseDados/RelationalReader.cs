using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SAPbobsCOM;
using SapFramework.BaseDados;
using SapFramework.BaseDados.enumeradores;
using SapFramework.Connections;
using SapFramework.dotNET.Atributos;


namespace SapFramework
{
    public static class RelationalReader
    {

        private static string mensagemErro = "Subtipo incorreto para {0} -- {1} -- {2}";


        //Configura tipo SAP baseado no tipo modelo.
        internal static void verificaTipos(Campos campo, PropertyInfo info, FieldsAttribute attribute, string nomeTabela)
        {
            #region Dados dos Atributos

            campo.DescricaoCampo = attribute.Descricao;
            campo.Mandatory = attribute.Obrigatorio ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
            campo.NomeCampo = attribute.Nome;
            campo.NomeTabela = nomeTabela;
            campo.UdoReferencia = attribute.LinkUdo;


            #endregion


            if (info.PropertyType == typeof(System.String))
            {
                switch (attribute.SubTipo)
                {

                    case BoFldSubTypes.st_None:

                        if (attribute.Tamanho > 254)
                        {
                            campo.TipoCampo = BoFieldTypes.db_Memo;
                            campo.SubTipos = BoFldSubTypes.st_None;
                        }
                        else
                        {
                            campo.TipoCampo = BoFieldTypes.db_Alpha;
                            campo.SubTipos = BoFldSubTypes.st_None;
                            campo.Tamanho = attribute.Tamanho;
                        }

                        break;

                    case BoFldSubTypes.st_Address:
                        campo.TipoCampo = BoFieldTypes.db_Alpha;
                        campo.SubTipos = BoFldSubTypes.st_Address;
                        break;

                    case BoFldSubTypes.st_Phone:
                        campo.TipoCampo = BoFieldTypes.db_Alpha;
                        campo.SubTipos = BoFldSubTypes.st_Phone;
                        break;

                    case BoFldSubTypes.st_Image:
                        campo.TipoCampo = BoFieldTypes.db_Alpha;
                        campo.SubTipos = BoFldSubTypes.st_Image;
                        break;

                    default:
                        B1Exception.writeLog(string.Format(mensagemErro, info.PropertyType.ToString(), attribute.Nome, attribute.TypeId));
                        break;

                }



            }
            else if (info.PropertyType == typeof(System.Int32) || info.PropertyType == typeof(System.Int16) || info.PropertyType == typeof(System.Int64))
            {
                switch (attribute.SubTipo)
                {
                    case BoFldSubTypes.st_None:
                        campo.TipoCampo = BoFieldTypes.db_Numeric;
                        campo.SubTipos = BoFldSubTypes.st_None;
                        campo.Tamanho = attribute.Tamanho;
                        break;

                    case BoFldSubTypes.st_Time:
                        campo.TipoCampo = BoFieldTypes.db_Date;
                        campo.SubTipos = BoFldSubTypes.st_Time;
                        break;

                    default:
                        B1Exception.writeLog(string.Format(mensagemErro, info.PropertyType.ToString(), attribute.Nome, attribute.TypeId));
                        break;
                }


            }
            else if (info.PropertyType == typeof(System.DateTime))
            {

                campo.TipoCampo = BoFieldTypes.db_Date;
                campo.SubTipos = BoFldSubTypes.st_None;

            }
            else if (info.PropertyType == typeof(System.Double))
            {

                switch (attribute.SubTipo)
                {
                    case BoFldSubTypes.st_None:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Sum;
                        break;

                    case BoFldSubTypes.st_Percentage:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Percentage;
                        break;

                    case BoFldSubTypes.st_Measurement:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Measurement;
                        break;

                    case BoFldSubTypes.st_Price:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Price;
                        break;

                    case BoFldSubTypes.st_Quantity:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Quantity;
                        break;

                    case BoFldSubTypes.st_Rate:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Rate;
                        break;

                    case BoFldSubTypes.st_Sum:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Sum;
                        break;

                    default:
                        B1Exception.writeLog(string.Format(mensagemErro, info.PropertyType.ToString(), attribute.Nome, attribute.TypeId));
                        break;
                }
                
            }
            else if (info.PropertyType == typeof(System.Decimal))
            {

                switch (attribute.SubTipo)
                {
                    case BoFldSubTypes.st_None:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Sum;
                        break;

                    case BoFldSubTypes.st_Percentage:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Percentage;
                        break;

                    case BoFldSubTypes.st_Measurement:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Measurement;
                        break;

                    case BoFldSubTypes.st_Price:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Price;
                        break;

                    case BoFldSubTypes.st_Quantity:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Quantity;
                        break;

                    case BoFldSubTypes.st_Rate:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Rate;
                        break;

                    case BoFldSubTypes.st_Sum:
                        campo.TipoCampo = BoFieldTypes.db_Float;
                        campo.SubTipos = BoFldSubTypes.st_Sum;
                        break;

                    default:
                        B1Exception.writeLog(string.Format(mensagemErro, info.PropertyType.ToString(), attribute.Nome, attribute.TypeId));
                        break;
                }

            }






        }


        internal static BoFldSubTypes RetornaSubTipo(string tipo)
        {

            switch (tipo)
            {
                case "":
                    return BoFldSubTypes.st_None;

                case "?":
                    return BoFldSubTypes.st_Address;

                case "#":
                    return BoFldSubTypes.st_Phone;

                case "T":
                    return BoFldSubTypes.st_Time;

                case "R":
                    return BoFldSubTypes.st_Rate;

                case "S":
                    return BoFldSubTypes.st_Sum;

                case "P":
                    return BoFldSubTypes.st_Price;

                case "Q":
                    return BoFldSubTypes.st_Quantity;

                case "%":
                    return BoFldSubTypes.st_Percentage;

                case "M":
                    return BoFldSubTypes.st_Measurement;

                case "B":
                    return BoFldSubTypes.st_Link;

                case "I":
                    return BoFldSubTypes.st_Image;

                default:
                    return BoFldSubTypes.st_None;
            }

        }

        internal static BoFieldTypes RetornaTipoCampo(string tipo)
        {

            switch (tipo)
            {
                case "M":
                    return BoFieldTypes.db_Memo;

                case "A":
                    return BoFieldTypes.db_Alpha;

                case "D":
                    return BoFieldTypes.db_Date;

                case "N":
                    return BoFieldTypes.db_Numeric;

                case "B":
                    return BoFieldTypes.db_Float;

                default:
                    return BoFieldTypes.db_Alpha;
            }

        }
    }
}
