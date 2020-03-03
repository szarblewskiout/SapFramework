﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbouiCOM;
using SapFramework.Connections;

namespace SapFramework.SAP.UI.Utils
{
    public class ChooseFromList
    {

        /// <summary>
        /// preenche campos da matrix / Código e Nome
        /// </summary>
        /// <param name="formUID">formUid do evento</param>
        /// <param name="pVal">pVal do evento</param>
        /// <param name="idMatrix">id da Matrix / Não pode ser grid</param>
        /// <param name="colunaChoose">Id da coluna onde esta o botão ChooseFromList</param>
        /// <param name="idChoose">Id do ChooseFromList</param>
        /// <param name="colunaChooseNome">Id da coluna onde deve ser preenchido o nome / retorna o campo 13 padrao / primeiro campo de cadastro de usuarios</param>
        /// <param name="mensagemErro">Mensagem de erro a ser apresentada se der algum problema</param>
        public static void PreencheCampos(string formUID, ItemEvent pVal, string idMatrix, string colunaChoose, string idChoose, string colunaChooseNome, string mensagemErro)
        {
            try
            {
                if (pVal.ItemUID == idMatrix && pVal.ColUID == colunaChoose)
                {
                    Form oForm = B1AppDomain.Application.Forms.Item(formUID);

                    IChooseFromListEvent oCFLEvento = (IChooseFromListEvent)pVal;

                    string sCFL_ID = oCFLEvento.ChooseFromListUID;

                    SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(sCFL_ID);

                    DataTable oDataTable = oCFLEvento.SelectedObjects;
                    Matrix oMatrix;
                    EditText oEditText;

                    try
                    {
                        oMatrix = oForm.Items.Item(idMatrix).Specific;

                        if (pVal.ColUID == colunaChoose && oCFL.UniqueID == idChoose)
                        {
                            oEditText = oMatrix.Columns.Item(colunaChooseNome).Cells.Item(pVal.Row).Specific;
                            oEditText.Value = oDataTable.GetValue(1, 0).ToString();
                            oEditText = oMatrix.Columns.Item(colunaChoose).Cells.Item(pVal.Row).Specific;
                            oEditText.Value = oDataTable.GetValue(0, 0).ToString();
                        }
                    }
                    catch
                    {
                    }

                    if (oForm.Mode != BoFormMode.fm_ADD_MODE)
                    {
                        oForm.Mode = BoFormMode.fm_UPDATE_MODE;
                    }
                }
            }
            catch (Exception ex)
            {
                //B1Exception.throwException(mensagemErro, ex);
            }
        }

        /// <summary>
        /// preenche campos da matrix / Código 
        /// </summary>
        /// <param name="formUID">formUid do evento</param>
        /// <param name="pVal">pVal do evento</param>
        /// <param name="idMatrix">id da Matrix / Não pode ser grid</param>
        /// <param name="colunaChoose">Id da coluna onde esta o botão ChooseFromList</param>
        /// <param name="idChoose">Id do ChooseFromList</param>
        /// <param name="mensagemErro">Mensagem de erro a ser apresentada se der algum problema</param>
        public static void PreencheCampos(string formUID, ItemEvent pVal, string idMatrix, string colunaChoose, string idChoose, string mensagemErro)
        {
            try
            {
                if (pVal.ItemUID == idMatrix && pVal.ColUID == colunaChoose)
                {
                    Form oForm = B1AppDomain.Application.Forms.Item(formUID);

                    IChooseFromListEvent oCFLEvento = (IChooseFromListEvent)pVal;

                    string sCFL_ID = oCFLEvento.ChooseFromListUID;

                    SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(sCFL_ID);

                    DataTable oDataTable = oCFLEvento.SelectedObjects;
                    Matrix oMatrix;
                    EditText oEditText;

                    try
                    {
                        oMatrix = oForm.Items.Item(idMatrix).Specific;

                        if (pVal.ColUID == colunaChoose && oCFL.UniqueID == idChoose)
                        {
                            
                            oEditText = oMatrix.Columns.Item(colunaChoose).Cells.Item(pVal.Row).Specific;
                            oEditText.Value = oDataTable.GetValue(0, 0).ToString();
                        }
                    }
                    catch
                    {
                    }

                    if (oForm.Mode != BoFormMode.fm_ADD_MODE)
                    {
                        oForm.Mode = BoFormMode.fm_UPDATE_MODE;
                    }
                }
            }
            catch (Exception ex)
            {
                //B1Exception.throwException(mensagemErro, ex);
            }
        }


        /// <summary>
        /// preenche campos Tela / Código e Nome
        /// </summary>
        /// <param name="formUID">formUid do evento</param>
        /// <param name="pVal">pVal do evento</param>
        /// <param name="idCampoChoose">Id do campo onde esta o choosefromlist</param>
        /// <param name="idChoose">Id do ChooseFromList</param>
        /// <param name="idCampoChooseNome">Id do campo onde deve ser preenchido o nome / retorna o campo 13 padrao / primeiro campo de cadastro de usuarios</param>
        /// <param name="mensagemErro">Mensagem de erro a ser apresentada se der algum problema</param>
        /// <param name="idCampoNomeDataTable">Id para buscar o nome a ser preenchido / como padrão retornara o 13</param>
        public static void PreencheCampos(string formUID, ItemEvent pVal, string idCampoChoose, string idChoose, string idCampoChooseNome, string mensagemErro, int idCampoNomeDataTable = 13)
        {
            try
            {
                if (pVal.ItemUID == idCampoChoose)
                {
                    Form oForm = B1AppDomain.Application.Forms.Item(formUID);

                    IChooseFromListEvent oCFLEvento = (IChooseFromListEvent)pVal;

                    string sCFL_ID = oCFLEvento.ChooseFromListUID;

                    SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(sCFL_ID);

                    DataTable oDataTable = oCFLEvento.SelectedObjects;
                    EditText oEditText;

                    try
                    {   
                        if (oCFL.UniqueID == idChoose)
                        {
                            oEditText = oForm.Items.Item(idCampoChooseNome).Specific;
                            oEditText.Value = oDataTable.GetValue(idCampoNomeDataTable, 0).ToString();
                            oEditText = oForm.Items.Item(idCampoChoose).Specific;
                            oEditText.Value = oDataTable.GetValue(0, 0).ToString();
                        }
                    }
                    catch
                    {
                    }

                    if (oForm.Mode != BoFormMode.fm_ADD_MODE)
                    {
                        oForm.Mode = BoFormMode.fm_UPDATE_MODE;
                    }
                }
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// preenche campos Tela / Código e Nome e lista de outros campos
        /// </summary>
        /// <param name="formUID">formUid do evento</param>
        /// <param name="pVal">pVal do evento</param>
        /// <param name="idCampoChoose">Id do campo onde esta o choosefromlist</param>
        /// <param name="idChoose">Id do ChooseFromList</param>
        /// <param name="idCampoChooseNome">Id do campo onde deve ser preenchido o nome / retorna o campo 13 padrao / primeiro campo de cadastro de usuarios</param>
        /// <param name="mensagemErro">Mensagem de erro a ser apresentada se der algum problema</param>
        /// <param name="listaCampos">Lista contendo os demais campos a serem preenchidos quando selecionado o choose</param>
        /// <param name="idCampoNomeDataTable">Id para buscar o nome a ser preenchido / como padrão retornara o 13</param>
        public static void PreencheCampos(string formUID, ItemEvent pVal, string idCampoChoose, string idChoose, string idCampoChooseNome, string mensagemErro, List<CamposPreencimento> listaCampos, int idCampoNomeDataTable = 13 )
        {
            try
            {
                if (pVal.ItemUID == idCampoChoose)
                {
                    Form oForm = B1AppDomain.Application.Forms.Item(formUID);

                    IChooseFromListEvent oCFLEvento = (IChooseFromListEvent)pVal;

                    string sCFL_ID = oCFLEvento.ChooseFromListUID;

                    SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(sCFL_ID);

                    DataTable oDataTable = oCFLEvento.SelectedObjects;
                    EditText oEditText;

                    try
                    {
                        if (oCFL.UniqueID == idChoose)
                        {
                            foreach(CamposPreencimento campo in listaCampos)
                            {
                                oEditText = oForm.Items.Item(campo.idCampo).Specific;
                                var valor = oDataTable.GetValue(campo.idDataTable, 0).ToString();

                                if (typeof(DateTime) == valor.GetType())
                                {
                                    oEditText.Value = InternalUtils.FormataData(valor);
                                }
                                else
                                {
                                    oEditText.Value = valor.ToString();
                                }


                            }


                            oEditText = oForm.Items.Item(idCampoChooseNome).Specific;
                            oEditText.Value = oDataTable.GetValue(idCampoNomeDataTable, 0).ToString();
                            oEditText = oForm.Items.Item(idCampoChoose).Specific;
                            oEditText.Value = oDataTable.GetValue(0, 0).ToString();
                        }
                    }
                    catch
                    {
                    }

                    if (oForm.Mode != BoFormMode.fm_ADD_MODE)
                    {
                        oForm.Mode = BoFormMode.fm_UPDATE_MODE;
                    }
                }
            }
            catch (Exception ex)
            {
                //B1Exception.throwException(mensagemErro, ex);
            }
        }

        /// <summary>
        /// preenche campos Tela / Código e lista de outros campos
        /// </summary>
        /// <param name="formUID">formUid do evento</param>
        /// <param name="pVal">pVal do evento</param>
        /// <param name="idCampoChoose">Id do campo onde esta o choosefromlist</param>
        /// <param name="idChoose">Id do ChooseFromList</param>
        /// <param name="mensagemErro">Mensagem de erro a ser apresentada se der algum problema</param>
        /// <param name="listaCampos">Lista contendo os demais campos a serem preenchidos quando selecionado o choose</param>
        
        public static void PreencheCampos(string formUID, ItemEvent pVal, string idCampoChoose, string idChoose, string mensagemErro, List<CamposPreencimento> listaCampos)
        {
            try
            {
                if (pVal.ItemUID == idCampoChoose)
                {
                    Form oForm = B1AppDomain.Application.Forms.Item(formUID);

                    IChooseFromListEvent oCFLEvento = (IChooseFromListEvent)pVal;

                    string sCFL_ID = oCFLEvento.ChooseFromListUID;

                    SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(sCFL_ID);

                    DataTable oDataTable = oCFLEvento.SelectedObjects;
                    EditText oEditText;

                    try
                    {
                        if (oCFL.UniqueID == idChoose)
                        {
                            foreach (CamposPreencimento campo in listaCampos)
                            {
                                oEditText = oForm.Items.Item(campo.idCampo).Specific;                             
                                var valor = oDataTable.GetValue(campo.idDataTable, 0);
                                
                                if(typeof(DateTime) == valor.GetType())
                                {
                                    oEditText.Value = InternalUtils.FormataData(valor);
                                }
                                else
                                {
                                    oEditText.Value = valor.ToString();
                                }

                                
                            }
                                                        
                            oEditText = oForm.Items.Item(idCampoChoose).Specific;
                            oEditText.Value = oDataTable.GetValue(0, 0).ToString();
                        }
                    }
                    catch(Exception ex)
                    {
                        //B1Exception.throwException(mensagemErro, ex);
                    }

                    if (oForm.Mode != BoFormMode.fm_ADD_MODE)
                    {
                        oForm.Mode = BoFormMode.fm_UPDATE_MODE;
                    }
                }
            }
            catch (Exception ex)
            {
                //B1Exception.throwException(mensagemErro, ex);
            }
        }


        /// <summary>
        /// preenche campos Tela / Somente o Código
        /// </summary>
        /// <param name="formUID">formUid do evento</param>
        /// <param name="pVal">pVal do evento</param>
        /// <param name="idCampoChoose">Id do campo onde esta o choosefromlist</param>
        /// <param name="idChoose">Id do ChooseFromList</param>
        /// <param name="mensagemErro">Mensagem de erro a ser apresentada se der algum problema</param>
        public static void PreencheCampos(string formUID, ItemEvent pVal, string idCampoChoose, string idChoose, string mensagemErro)
        {
            try
            {
                if (pVal.ItemUID == idCampoChoose)
                {
                    Form oForm = B1AppDomain.Application.Forms.Item(formUID);

                    IChooseFromListEvent oCFLEvento = (IChooseFromListEvent)pVal;

                    string sCFL_ID = oCFLEvento.ChooseFromListUID;

                    SAPbouiCOM.ChooseFromList oCFL = oForm.ChooseFromLists.Item(sCFL_ID);

                    DataTable oDataTable = oCFLEvento.SelectedObjects;
                    EditText oEditText;

                    try
                    {
                        if (oCFL.UniqueID == idChoose)
                        {
                            oEditText = oForm.Items.Item(idCampoChoose).Specific;
                            oEditText.Value = oDataTable.GetValue(0, 0).ToString();
                        }
                    }
                    catch
                    {
                    }

                    if (oForm.Mode != BoFormMode.fm_ADD_MODE)
                    {
                        oForm.Mode = BoFormMode.fm_UPDATE_MODE;
                    }
                }
            }
            catch (Exception ex)
            {
                //B1Exception.throwException(mensagemErro, ex);
            }
        }

    }

    public class CamposPreencimento
    {
       public object idDataTable { get; set; }
       public string idCampo { get; set; }
        
    }


}
