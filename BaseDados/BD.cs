using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM;
using SapFramework.BaseDados.enumeradores;
using SapFramework.BaseDados.Sistema;
using SapFramework.Connections;
using SapFramework.SAP.DI.Utils;


namespace SapFramework.BaseDados
{
    public static class BD
    {

        public enum OperacoesBd
        {
            Adiciona,
            Atualiza,
            Deleta
        }

        internal static bool controleUdo { get; set; }

        public static void AtualizaBd()
        {
            controleUdo = false;
            B1AppDomain.Application.SetStatusBarMessage("Iniciando verificação de estrutura de dados...", BoMessageTime.bmt_Short, false);
            int intRetCode = -1;
            var listaTabelas = OUTB.RetornaValores();
            


            int qtdeCampos = 0;

            B1AppDomain.Application.SetStatusBarMessage("Verificando Estrutura de Dados...", BoMessageTime.bmt_Short, false);


            #region Cria Tabelas
            var pbTabelas = B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Tabelas", B1AppDomain.DicionarioTabelasCampos.Count, false);
            foreach (KeyValuePair<object, Tabelas> tabela in B1AppDomain.DicionarioTabelasCampos.Where(p => p.Value.Ttabela == Tipos.TipoTabela.Usuario))
            {
                pbTabelas.Value++;
                pbTabelas.Text = tabela.Value.NomeTabela;
                qtdeCampos = qtdeCampos + tabela.Value.Campos.Count;
                //Caso nao exista registro da tabela na base sap
                if (listaTabelas.All(p => p.TableName != tabela.Value.NomeTabela))
                {
                    AddTabela(tabela.Value.NomeTabela, tabela.Value.DescricaoTabela, tabela.Value.TipoTabelaSap);
                }

            }
            pbTabelas.Stop();
            Objects.LimpaMemoria(pbTabelas);
            #endregion


            #region Cria Campos

            listaTabelas = new List<OUTB>();
            listaTabelas = OUTB.RetornaValores();
            var listaCampos = new List<CUFD>();

            var pbCampos = B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Campos", qtdeCampos, false);

            foreach (KeyValuePair<object, Tabelas> tabela in B1AppDomain.DicionarioTabelasCampos)
            {
                int existeTabela = 0;

                existeTabela = tabela.Value.Ttabela == Tipos.TipoTabela.Sap ? 1 : listaTabelas.Where(p => p.TableName.ToUpper() == tabela.Value.NomeTabela.ToUpper()).Count();


                if (existeTabela > 0)
                {
                    listaCampos = tabela.Value.Ttabela == Tipos.TipoTabela.Sap ? CUFD.RetornaValores(tabela.Value.NomeTabela) : CUFD.RetornaValores("@" + tabela.Value.NomeTabela);
                    
                    //Varre campos da tabela para criacao
                    foreach (Campos campo in tabela.Value.Campos)
                    {
                        pbCampos.Value++;
                        pbCampos.Text = campo.NomeTabela + " - " + campo.NomeCampo;

                        


                        if (listaCampos.Count(p => p.AliasID.ToUpper() == campo.NomeCampo.ToUpper() && p.TableID.ToUpper() == (tabela.Value.Ttabela == Tipos.TipoTabela.Sap ? campo.NomeTabela.ToUpper() : "@" + campo.NomeTabela.ToUpper())) <= 0)
                        {
                            AddCampo(campo.NomeCampo,
                                 campo.NomeTabela,
                                 campo.TipoCampo,
                                 campo.SubTipos,
                                 campo.Tamanho,
                                 campo.Mandatory,
                                 campo.DescricaoCampo,
                                 campo.ValorPadrao,
                                 campo.ValoresValidos,
                                 tabela.Value.Ttabela,
                                 campo.UdoReferencia,
                                 campo.TabelaReferencia);
                        }
                        else
                        {

                            if (VerificaCampo(campo, listaCampos.Where(p => p.AliasID.ToUpper() == campo.NomeCampo.ToUpper() && p.TableID.ToUpper() == (tabela.Value.Ttabela == Tipos.TipoTabela.Sap ? campo.NomeTabela.ToUpper() : "@" + campo.NomeTabela.ToUpper())).SingleOrDefault()))
                            {
                                intRetCode = tabela.Value.Ttabela == Tipos.TipoTabela.Usuario ? DeleteCampo("@" + campo.NomeTabela, campo.NomeCampo) : DeleteCampo(campo.NomeTabela, campo.NomeCampo);


                                if (intRetCode == 0)
                                {
                                    AddCampo(campo.NomeCampo,
                                     campo.NomeTabela,
                                     campo.TipoCampo,
                                     campo.SubTipos,
                                     campo.Tamanho,
                                     campo.Mandatory,
                                     campo.DescricaoCampo,
                                     campo.ValorPadrao,
                                     campo.ValoresValidos,
                                     tabela.Value.Ttabela,
                                     campo.UdoReferencia,
                                     campo.TabelaReferencia);
                                }
                            }
                        }


                    }
                }
            }

            pbCampos.Stop();
            Objects.LimpaMemoria(pbCampos);

            #endregion


            #region Cria Udo
            var pbUdos = B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Registrando Udos", B1AppDomain.DicionarioUdos.Count, false);
            foreach (KeyValuePair<object, Udo> udo in B1AppDomain.DicionarioUdos)
            {
                pbUdos.Value++;

                var tabela = B1AppDomain.DicionarioTabelasCampos.Where(p => p.Value.NomeTabela == udo.Value.TableName)
                    .Select(p => p.Value)
                    .SingleOrDefault();

                tabela.Udos = udo.Value;
                foreach (KeyValuePair<object, UdoFilhos> filho in B1AppDomain.DicionarioUdosFilhos.Where(p => p.Value.TabelaPai == udo.Value.TableName))
                {
                    if (tabela.Udos.Filhos == null)
                    {
                        tabela.Udos.Filhos = new List<UdoFilhos>();
                    }
                    tabela.Udos.Filhos.Add(filho.Value);
                }

                AddUdo(tabela);

            }

            pbUdos.Stop();
            Objects.LimpaMemoria(pbUdos);
            #endregion

            B1AppDomain.Application.SetStatusBarMessage("Verificação de estrutura de dados concluida!", BoMessageTime.bmt_Short, false);

            if (controleUdo)
            {
                B1AppDomain.Application.MessageBox("Dados foram alterados o sistema será reiniciado");
                B1AppDomain.Company.Disconnect();
                B1AppDomain.Application.Menus.Item("3329").Activate();
                B1AppDomain.Application.Forms.ActiveForm.Items.Item("3").Click();
            }



        }


        internal static bool VerificaCampo(Campos campo, CUFD registro)
        {
            
            bool mand = registro.NotNull == "Y";
            if (mand != (campo.Mandatory == BoYesNoEnum.tYES) && campo.TipoCampo != BoFieldTypes.db_Date && campo.TipoCampo != BoFieldTypes.db_Memo)
            {
                return true;
            }
            if (campo.DescricaoCampo != registro.Descr)
            {
                return true;
            }
            if (campo.NomeCampo != registro.AliasID)
            {
                return true;
            }
            if (campo.SubTipos != RelationalReader.RetornaSubTipo(registro.EditType))
            {
                return true;
            }
            if (campo.TabelaReferencia != null)
            {
                if (campo.TabelaReferencia != registro.RTable)
                {
                    return true;
                }
            }
            
            if (campo.Tamanho != registro.EditSize && campo.TipoCampo == BoFieldTypes.db_Alpha && campo.TipoCampo == BoFieldTypes.db_Numeric)
            {
                return true;
            }

            if (campo.ValorPadrao != null)
            {
                if (campo.ValorPadrao != registro.Dflt)
                {
                    return true;
                }
            }
            
            if (campo.TipoCampo != RelationalReader.RetornaTipoCampo(registro.TypeID))
            {
                return true;
            }
            if(campo.UdoReferencia != null)
            {
                if (campo.UdoReferencia != registro.RelUDO)
                {
                    return true;
                }
            }

            if (campo.ValoresValidos != null)
            {

                foreach(UFD1 valor in UFD1.RetornaValores(campo.NomeTabela, campo.NomeCampo))
                {
                    if(campo.ValoresValidos.Where(p => p.Valor == valor.FldValue && p.Descricao == valor.Descr).Count() <= 0)
                    {
                        return true;
                    }
                }


                //foreach (ValoresValidos vlr in campo.ValoresValidos)
                //{
                //    if (UFD1.RetornaValores(campo.NomeTabela, campo.NomeCampo).Where(p => p.FldValue == vlr.Valor && p.Descr == vlr.Descricao).Count() <= 0)
                //    {
                //        return true;
                //    }

                    
                //}

            }
            
                


            return false;
        }

        public static int Tabela(List<Tabelas> tabelas)
        {
            var listaTabelas = OUTB.RetornaValores();
            int intRetCode = -1;
            foreach (Tabelas tabela in tabelas)
            {

                if (listaTabelas.All(p => p.TableName != tabela.NomeTabela))
                {
                    intRetCode = Tabela(tabela);
                }

            }

            return intRetCode;
        }

        public static int Tabela(Tabelas tabela)
        {
            int intRetCode = -1;

            try
            {
                #region Tabela de usuários
                if (tabela.Ttabela == Tipos.TipoTabela.Usuario)
                {
                    if (tabela.Operacao == OperacoesBd.Adiciona)
                    {
                        intRetCode = AddTabela(tabela.NomeTabela, tabela.DescricaoTabela, tabela.TipoTabelaSap);

                        if (intRetCode == 0 || intRetCode == -2035)
                        {
                            foreach (Campos campo in tabela.Campos)
                            {
                                if (campo.Operacao == OperacoesBd.Adiciona)
                                {
                                    intRetCode = AddCampo(campo.NomeCampo, campo.NomeTabela, campo.TipoCampo,
                                                           campo.SubTipos, campo.Tamanho, campo.Mandatory,
                                                           campo.DescricaoCampo, campo.ValorPadrao, campo.ValoresValidos,
                                                           tabela.Ttabela, campo.UdoReferencia, campo.TabelaReferencia);

                                }

                            }
                        }

                    }
                    else if (tabela.Operacao == OperacoesBd.Atualiza)
                    {

                        intRetCode = UpdateTabela(tabela.NomeTabela, tabela.DescricaoTabela);

                        if (intRetCode == 0 || intRetCode == -2035)
                        {
                            foreach (Campos campo in tabela.Campos)
                            {
                                if (campo.Operacao == OperacoesBd.Adiciona)
                                {
                                    intRetCode = AddCampo(campo.NomeCampo, campo.NomeTabela, campo.TipoCampo,
                                                           campo.SubTipos, campo.Tamanho, campo.Mandatory,
                                                           campo.DescricaoCampo, campo.ValorPadrao, campo.ValoresValidos,
                                                           tabela.Ttabela, campo.UdoReferencia, campo.TabelaReferencia);
                                }
                                else if (campo.Operacao == OperacoesBd.Atualiza)
                                {
                                    intRetCode = UpdateCampo(campo.NomeTabela, campo.NomeCampo, campo.Tamanho,
                                                              campo.Mandatory, campo.DescricaoCampo, campo.ValorPadrao,
                                                              campo.ValoresValidos);
                                }
                                else if (campo.Operacao == OperacoesBd.Deleta)
                                {
                                    intRetCode = DeleteCampo(campo.NomeTabela, campo.NomeCampo);
                                }


                            }
                        }

                    }
                    else if (tabela.Operacao == OperacoesBd.Deleta)
                    {
                        intRetCode = DeleteTabela(tabela.NomeTabela);
                    }



                }
                #endregion
                #region Tabela do Sistema
                else
                {
                    foreach (Campos campo in tabela.Campos)
                    {
                        if (campo.Operacao == OperacoesBd.Adiciona)
                        {
                            intRetCode = AddCampo(campo.NomeCampo, campo.NomeTabela, campo.TipoCampo,
                                                   campo.SubTipos, campo.Tamanho, campo.Mandatory,
                                                   campo.DescricaoCampo, campo.ValorPadrao, campo.ValoresValidos,
                                                   tabela.Ttabela, campo.UdoReferencia, campo.TabelaReferencia);
                        }
                        else if (campo.Operacao == OperacoesBd.Atualiza)
                        {
                            intRetCode = UpdateCampo(campo.NomeTabela, campo.NomeCampo, campo.Tamanho,
                                                      campo.Mandatory, campo.DescricaoCampo, campo.ValorPadrao,
                                                      campo.ValoresValidos);
                        }
                        else if (campo.Operacao == OperacoesBd.Deleta)
                        {
                            intRetCode = DeleteCampo(campo.NomeTabela, campo.NomeCampo);
                        }
                    }
                }
                #endregion


            }
            catch (Exception ex)
            {
                B1Exception.throwException("Erro Tabela :: ", ex);
            }


            return intRetCode;
        }

        public static int Udos(Tabelas tabela, List<Tabelas> listaParaFilhos)
        {
            int intRetCode = -1;

            try
            {

                if (tabela.Udos != null)
                {
                    if (tabela.Udos.Operacao == OperacoesBd.Adiciona)
                    {
                        //AddUdo(tabela, listaParaFilhos);
                    }
                    else if (tabela.Udos.Operacao == OperacoesBd.Atualiza)
                    {
                        DeleteUdo(tabela);
                        //AddUdo(tabela, listaParaFilhos);
                    }
                }


            }
            catch (Exception ex)
            {

                B1Exception.throwException("Erro ao Criar Udos :: " + tabela.Udos.TableName + " - ", ex);
            }


            return intRetCode;
        }

        public static int Udos(List<Tabelas> tabelas)
        {
            int intRetCode = -1;
            foreach (Tabelas tabela in tabelas)
            {
                intRetCode = Udos(tabela, tabelas);
            }

            return intRetCode;
        }



        internal static int idCampo(string nomeCampo, string tabela)
        {
            var _rset = (Recordset)B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            var sql = string.Format(@"select fieldid from CUFD where AliasID = '{0}' and TableID = '{1}'", nomeCampo, tabela);
            _rset.DoQuery(sql);

            string resultado = _rset.Fields.Item(0).Value.ToString();
            Marshal.FinalReleaseComObject(_rset);
            GC.Collect();

            return Convert.ToInt32(resultado);
        }


        #region Tabela

        internal static int DeleteTabela(string nomeTabela)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserTablesMD objUserTablesMD = null;

            try
            {
                //instancia objeto para deletar tabela
                objUserTablesMD = (UserTablesMD)B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);
                if (objUserTablesMD.GetByKey(nomeTabela))
                {

                    //deleta tabela
                    intRetCode = objUserTablesMD.Remove();
                    //verifica e retorna erro
                    if (intRetCode != 0 && intRetCode != -2035)
                    {
                        B1Exception.throwException("MetaData.CriaCampos: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
                    }

                }
                else
                {
                    B1AppDomain.Application.SetStatusBarMessage("Tabela não localizada no sistema :: " + nomeTabela, BoMessageTime.bmt_Short, true);
                }


            }
            catch (Exception ex)
            {

                B1Exception.throwException("Erro ao Deletar Tabela :: " + nomeTabela + " - ", ex);
            }



            return intRetCode;
        }

        internal static int UpdateTabela(string nomeTabela, string novaDescricao)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserTablesMD objUserTablesMD = null;

            try
            {
                //instancia objeto para atualizar tabela
                objUserTablesMD = (UserTablesMD)B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);
                if (objUserTablesMD.GetByKey(nomeTabela))
                {

                    objUserTablesMD.TableDescription = novaDescricao;

                    //atualiza tabela
                    intRetCode = objUserTablesMD.Update();
                    //verifica e retorna erro
                    if (intRetCode != 0 && intRetCode != -2035)
                    {
                        B1Exception.throwException("MetaData.CriaCampos: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
                    }

                }
                else
                {
                    B1AppDomain.Application.SetStatusBarMessage("Tabela não localizada no sistema :: " + nomeTabela, BoMessageTime.bmt_Short, true);
                }


            }
            catch (Exception ex)
            {

                B1Exception.throwException("Erro ao Atualizar Tabela :: " + nomeTabela + " - ", ex);
            }



            return intRetCode;
        }

        internal static int AddTabela(string nomeTabela, string descricao, BoUTBTableType tipoTabela)
        {

            int intRetCode = -1;
            SAPbobsCOM.UserTablesMD objUserTablesMD = null;

            try
            {
                //instancia objeto para criar tabela
                objUserTablesMD = (UserTablesMD)B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);

                //seta propriedades
                objUserTablesMD.TableName = nomeTabela;
                objUserTablesMD.TableDescription = descricao;
                objUserTablesMD.TableType = tipoTabela;

                //adiciona tabela
                intRetCode = objUserTablesMD.Add();
                //verifica e retorna erro
                if (intRetCode != 0 && intRetCode != -2035)
                {
                    B1Exception.throwException("MetaData.CriaCampos: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
                }

                //mata objeto para reutilizar senao trava
                //Marshal.FinalReleaseComObject(objUserTablesMD);
                //objUserTablesMD = null;
                //GC.Collect();
                Objects.LimpaMemoria(objUserTablesMD);


            }
            catch (Exception ex)
            {
                B1Exception.throwException("Erro ao Criar Tabela :: " + nomeTabela + " - ", ex);

            }

            return intRetCode;
        }

        #endregion


        #region Campos

        internal static int AddCampo(string nomeCampo, string nomeTabela, BoFieldTypes tipoCampo, BoFldSubTypes subTipo, int tamanho, BoYesNoEnum mandatory, string descricao, string valorPadrao, List<ValoresValidos> valoresValidos, Tipos.TipoTabela tipoTabela, string nomeUdoReferencia = "", string nomeTabelaReferencia = "")
        {

            int intRetCode = -1;

            SAPbobsCOM.UserFieldsMD objUserFieldsMD = null;

            //instancia objeto para criar campo
            objUserFieldsMD = (UserFieldsMD)B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

            //seta propriedades
            objUserFieldsMD.Name = nomeCampo;
            objUserFieldsMD.TableName = tipoTabela == Tipos.TipoTabela.Usuario ? "@" + nomeTabela : nomeTabela;
            objUserFieldsMD.Type = tipoCampo;
            objUserFieldsMD.SubType = subTipo;
            objUserFieldsMD.EditSize = tamanho;
            objUserFieldsMD.Mandatory = (BoYesNoEnum)mandatory;
            objUserFieldsMD.Description = descricao;
            objUserFieldsMD.AddValidValues(valoresValidos);
            if (!string.IsNullOrEmpty(nomeUdoReferencia))
            {
                objUserFieldsMD.LinkedUDO = nomeUdoReferencia;
            }

            if (!string.IsNullOrEmpty(nomeTabelaReferencia))
            {
                objUserFieldsMD.LinkedTable = nomeTabelaReferencia;
            }

            
            objUserFieldsMD.DefaultValue = valorPadrao;
            //adiciona campo
            intRetCode = objUserFieldsMD.Add();
            //verifica e retorna erro
            if (intRetCode != 0 && intRetCode != -2035)
            {
                B1Exception.throwException("MetaData.CriaCampos: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
            }

            //mata objeto para reutilizar senao trava
            Marshal.FinalReleaseComObject(objUserFieldsMD);
            objUserFieldsMD = null;
            GC.Collect();

            return intRetCode;

        }

        internal static int UpdateCampo(string nomeTabela, string nomeCampo, int tamanho, BoYesNoEnum mandatory, string descricaoCampo, string valorPadrao, List<ValoresValidos> valoresValidos)
        {
            int intRetCode = -1;

            SAPbobsCOM.UserFieldsMD objUserFieldsMD = null;

            //instancia objeto para alterar campo
            objUserFieldsMD = (UserFieldsMD)B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);


            if (objUserFieldsMD.GetByKey(nomeTabela, idCampo(nomeCampo, nomeTabela)))
            {

                //seta propriedades
                objUserFieldsMD.EditSize = tamanho;
                objUserFieldsMD.Mandatory = mandatory;
                objUserFieldsMD.Description = descricaoCampo;
                objUserFieldsMD.DefaultValue = valorPadrao;
                objUserFieldsMD.AddValidValues(valoresValidos);


                //Atualiza Campos campo
                intRetCode = objUserFieldsMD.Update();
                //verifica e retorna erro
                if (intRetCode != 0 && intRetCode != -2035)
                {
                    B1Exception.throwException("MetaData.CriaCampos: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
                }


            }

            //mata objeto para reutilizar senao trava
            Marshal.FinalReleaseComObject(objUserFieldsMD);
            objUserFieldsMD = null;
            GC.Collect();

            return intRetCode;
        }

        internal static int DeleteCampo(string nomeTabela, string nomeCampo)
        {
            int intRetCode = -1;

            SAPbobsCOM.UserFieldsMD objUserFieldsMD = null;

            //instancia objeto para criar campo
            objUserFieldsMD = (UserFieldsMD)B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

            if (objUserFieldsMD.GetByKey(nomeTabela, idCampo(nomeCampo, nomeTabela)))
            {

                intRetCode = objUserFieldsMD.Remove();
                //verifica e retorna erro
                if (intRetCode != 0 && intRetCode != -2035)
                {
                    B1Exception.throwException("MetaData.CriaCampos: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
                }


            }

            //mata objeto para reutilizar senao trava
            Marshal.FinalReleaseComObject(objUserFieldsMD);
            objUserFieldsMD = null;
            GC.Collect();

            return 0;
        }

        #endregion


        #region UDOs

        internal static int DeleteUdo(Tabelas tabela)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;


            if (oUserObjectMD.GetByKey(tabela.Udos.TableName))
            {

                intRetCode = oUserObjectMD.Remove();

                //verifica e retorna erro
                if (intRetCode != 0 && intRetCode != -2035)
                {
                    //B1Exception.throwException("MetaData.CriaCampos: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
                }


            }

            return intRetCode;
        }

        internal static int AddUdo(Tabelas tabela)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;

            oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
            oUserObjectMD.CanCancel = tabela.Udos.Cancel;
            oUserObjectMD.CanClose = tabela.Udos.Close;
            oUserObjectMD.CanCreateDefaultForm = tabela.Udos.CreateDefaultForm;
            oUserObjectMD.EnableEnhancedForm = tabela.Udos.EnableEnhancedform;
            if (tabela.Udos.CreateDefaultForm == SAPbobsCOM.BoYesNoEnum.tYES)
            {
                if (tabela.Udos.ObjectType != BoUDOObjType.boud_Document)
                {
                    oUserObjectMD.FormColumns.FormColumnAlias = "Code";
                    oUserObjectMD.FormColumns.FormColumnDescription = "Code";

                }
                else
                {
                    oUserObjectMD.FormColumns.FormColumnAlias = "DocEntry";
                    oUserObjectMD.FormColumns.FormColumnDescription = "DocEntry";

                }

                int voltaCampos = 1;
                foreach (Campos campo in tabela.Campos)
                {

                    oUserObjectMD.FormColumns.Add();
                    oUserObjectMD.FormColumns.SetCurrentLine(voltaCampos);
                    oUserObjectMD.FormColumns.FormColumnAlias = "U_" + campo.NomeCampo;
                    oUserObjectMD.FormColumns.FormColumnDescription = campo.DescricaoCampo;
                    oUserObjectMD.FormColumns.Editable = BoYesNoEnum.tYES;
                    oUserObjectMD.FormColumns.SonNumber = 0;
                    voltaCampos++;


                }

            }

            oUserObjectMD.CanDelete = tabela.Udos.Delete;
            oUserObjectMD.CanFind = tabela.Udos.Find;
            if (tabela.Udos.Find == SAPbobsCOM.BoYesNoEnum.tYES)
            {
                if (tabela.Udos.ObjectType != BoUDOObjType.boud_Document)
                {
                    oUserObjectMD.FindColumns.ColumnAlias = "Code";
                    oUserObjectMD.FindColumns.ColumnDescription = "Code";
                    oUserObjectMD.FindColumns.Add();
                    oUserObjectMD.FindColumns.ColumnAlias = "Name";
                    oUserObjectMD.FindColumns.ColumnDescription = "Name";
                }
                else
                {
                    oUserObjectMD.FindColumns.ColumnAlias = "DocEntry";
                    oUserObjectMD.FindColumns.ColumnDescription = "DocEntry";
                    oUserObjectMD.FindColumns.ColumnAlias = "DocNum";
                    oUserObjectMD.FindColumns.ColumnDescription = "DocNum";

                }

                int voltaFind = 1;
                foreach (Campos campo in tabela.Campos)
                {

                    oUserObjectMD.FindColumns.Add();
                    oUserObjectMD.FindColumns.SetCurrentLine(voltaFind);
                    oUserObjectMD.FindColumns.ColumnAlias = "U_" + campo.NomeCampo;
                    oUserObjectMD.FindColumns.ColumnDescription = campo.DescricaoCampo;
                    voltaFind++;


                }
            }


            oUserObjectMD.CanYearTransfer = tabela.Udos.YearTransfer;
            oUserObjectMD.Code = tabela.Udos.Code;
            oUserObjectMD.ManageSeries = tabela.Udos.ManageSeries;
            oUserObjectMD.Name = tabela.Udos.Name;
            oUserObjectMD.ObjectType = tabela.Udos.ObjectType;
            oUserObjectMD.TableName = tabela.Udos.TableName;

            int numerofilho = 1;

            if (tabela.Udos.Filhos != null)
            {
                foreach (UdoFilhos listaUdo in tabela.Udos.Filhos)
                {

                    if (numerofilho > 1)
                    {
                        oUserObjectMD.ChildTables.Add();

                    }

                    oUserObjectMD.ChildTables.TableName = listaUdo.TableName;
                    oUserObjectMD.ChildTables.ObjectName = listaUdo.TableName;


                    if (tabela.Udos.EnableEnhancedform == BoYesNoEnum.tNO)
                    {

                        foreach (Tabelas tabelaFilho in B1AppDomain.DicionarioTabelasCampos.Where(p => p.Value.NomeTabela == listaUdo.TableName).Select(p => p.Value))
                        {
                            int voltaFilhos = 1;
                            foreach (Campos campo in tabelaFilho.Campos)
                            {


                                oUserObjectMD.FormColumns.Add();
                                oUserObjectMD.FormColumns.SetCurrentLine(voltaFilhos);
                                oUserObjectMD.FormColumns.FormColumnAlias = "U_" + campo.NomeCampo;
                                oUserObjectMD.FormColumns.FormColumnDescription = campo.DescricaoCampo;
                                oUserObjectMD.FormColumns.Editable = BoYesNoEnum.tYES;
                                oUserObjectMD.FormColumns.SonNumber = numerofilho;
                                voltaFilhos++;


                            }


                        }


                    }
                    else
                    {
                        foreach (Tabelas tabelaFilho in B1AppDomain.DicionarioTabelasCampos.Where(p => p.Value.NomeTabela == listaUdo.TableName).Select(p => p.Value))
                        {
                            int voltaNovo = 0;
                            foreach (Campos campo in tabelaFilho.Campos)
                            {

                                if (voltaNovo > 0)
                                {
                                    oUserObjectMD.EnhancedFormColumns.Add();
                                }

                                oUserObjectMD.EnhancedFormColumns.SetCurrentLine(voltaNovo);
                                oUserObjectMD.EnhancedFormColumns.ColumnAlias = "U_" + campo.NomeCampo;
                                oUserObjectMD.EnhancedFormColumns.ColumnDescription = campo.DescricaoCampo;
                                oUserObjectMD.EnhancedFormColumns.Editable = BoYesNoEnum.tYES;
                                oUserObjectMD.EnhancedFormColumns.ColumnIsUsed = BoYesNoEnum.tYES;
                                oUserObjectMD.EnhancedFormColumns.ChildNumber = numerofilho;
                                voltaNovo++;


                            }
                        }
                    }

                    numerofilho++;

                }
            }



            oUserObjectMD.RebuildEnhancedForm = tabela.Udos.RebuildEnhancedForm;

            oUserObjectMD.FormSRF = tabela.Udos.Formulario;

            intRetCode = oUserObjectMD.Add();

            //mata objeto para reutilizar senao trava
            Marshal.FinalReleaseComObject(oUserObjectMD);
            oUserObjectMD = null;
            GC.Collect();


            //verifica e retorna erro
            if (intRetCode != 0 && intRetCode != -2035)
            {
                // B1Exception.throwException("MetaData.CriaCampos: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
            }
            else if (intRetCode == 0)
            {
                controleUdo = true;
            }
            //else if(intRetCode == 0)
            //{
            //    oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
            //    if(oUserObjectMD.GetByKey(tabela.Udos.Code))
            //    {
            //        oUserObjectMD.RebuildEnhancedForm = BoYesNoEnum.tNO;
            //        oUserObjectMD.FormSRF = tabela.Udos.Formulario;
            //        oUserObjectMD.Update(); 

            //    }

            //    //mata objeto para reutilizar senao trava
            //    Marshal.FinalReleaseComObject(oUserObjectMD);
            //    oUserObjectMD = null;
            //    GC.Collect();

            //}

            return intRetCode;
        }

        #endregion


        
    }
}
