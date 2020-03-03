using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SapFramework.BaseDados;
using SapFramework.BaseDados.enumeradores;
using SapFramework.Connections;
using SapFramework.dotNET.Atributos;
using SAPbobsCOM;

namespace SapFramework.SAP.UI
{
    public abstract class TableBase
    {
        public string Code { get; set; }
        public string Name { get; set; }

        private readonly List<string> TableId;

        protected TableBase()
        {

            TableId = new List<string>();
            TablesAttribute attribute = null;
            FieldsAttribute flAttribute = null;
            UdoAttribute udoAttribute = null;
            UdoChildAttribute udoChildAttribute = null;
            ValidValuesAttribute valoresValidos = null;

            foreach (object obj2 in base.GetType().GetCustomAttributes(false))
            {

                #region Atributo Tabelas

                if (obj2 is TablesAttribute)
                {
                    attribute = obj2 as TablesAttribute;

                    if (!string.IsNullOrEmpty(attribute.Nome))
                    {
                        Tabelas tb = new Tabelas();
                        tb.NomeTabela = attribute.Nome;
                        tb.DescricaoTabela = attribute.Descricao;
                        tb.TipoTabelaSap = attribute.Tipo;
                        tb.Campos = new List<Campos>();
                        tb.Ttabela = attribute.TabelaSistema ? Tipos.TipoTabela.Sap : Tipos.TipoTabela.Usuario;
                        
                        foreach (PropertyInfo info in this.GetType().GetProperties())
                        {
                            List<ValoresValidos> vlrs = new List<ValoresValidos>();
                            Campos cp = new Campos();
                            foreach (object field in info.GetCustomAttributes(true))
                            {
                                
                                if (field is FieldsAttribute)
                                {
                                    flAttribute = field as FieldsAttribute;

                                    RelationalReader.verificaTipos(cp, info, flAttribute, tb.NomeTabela);


                                }

                                if (field is ValidValuesAttribute)
                                {
                                    valoresValidos = field as ValidValuesAttribute;
                                    vlrs.Add(new ValoresValidos() { Descricao = valoresValidos.Descricao, Valor = valoresValidos.Valor });
                                }


                            }

                            if (!string.IsNullOrEmpty(cp.NomeCampo))
                            {
                                if (vlrs.Count > 0)
                                {
                                    cp.ValoresValidos = vlrs;
                                }
                                tb.Campos.Add(cp);
                            }

                            

                        }


                        B1AppDomain.RegisterTable(this, tb);

                    }

                }

                #endregion

                #region Atributo Udo

                if (obj2 is UdoAttribute)
                {

                    udoAttribute = obj2 as UdoAttribute;

                    if (!string.IsNullOrEmpty(udoAttribute.Code))
                    {
                        Udo ud = new Udo();
                        ud.TableName = udoAttribute.TableName;
                        ud.Name = udoAttribute.Name;
                        ud.Code = udoAttribute.Code;
                        ud.Cancel = udoAttribute.Cancel;
                        ud.Close = udoAttribute.Close;
                        ud.CreateDefaultForm = udoAttribute.CreateDefaultForm;
                        ud.Delete = udoAttribute.Delete;
                        ud.Find = udoAttribute.Find;
                        ud.YearTransfer = udoAttribute.YearTransfer;
                        ud.ManageSeries = udoAttribute.ManageSeries;
                        ud.ObjectType = udoAttribute.ObjectType;
                        ud.Formulario = udoAttribute.Formulario;
                        ud.EnableEnhancedform = udoAttribute.EnableEnhancedform;
                        ud.RebuildEnhancedForm = udoAttribute.RebuildEnhancedForm;


                        B1AppDomain.RegisterUdo(this, ud);
                    }

                }

                #endregion

                #region Atributo UdoFilhos

                if (obj2 is UdoChildAttribute)
                {
                    udoChildAttribute = obj2 as UdoChildAttribute;

                    if (!string.IsNullOrEmpty(udoChildAttribute.TableName))
                    {
                        UdoFilhos udf = new UdoFilhos();
                        udf.TableName = udoChildAttribute.TableName;
                        udf.TabelaPai = udoChildAttribute.TabelaPai;

                        B1AppDomain.RegisterUdoChild(this, udf);
                    }

                }

                #endregion



            }
            if (attribute == null)
            {
                B1Exception.writeLog("Falha ao indexar Tabela. Por favor checar os atributos informados");
            }



        }

    }
}
