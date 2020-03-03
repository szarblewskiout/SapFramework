using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbouiCOM;
using SapFramework.Connections;
using SapFramework.dotNET.Atributos;
using SapFramework.SAP.Events;

namespace SapFramework.SAP.UI
{
    public abstract class FormBase 
    {

        private readonly List<string> formUid;
        public Form oForm;


        protected Item GetItem(string uid)
        {
            //CapturaFormulario();
            return this.oForm.Items.Item(uid);
        }


        private void CapturaFormulario()
        {
            if (formUid != null)
            {
                oForm = B1AppDomain.Application.Forms.Item(this.formUid);
            }
            else
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
            }
        }


        protected FormBase()
        {
            formUid = new List<string>();
            FormAttribute attribute = null;
            int index = 0;
            foreach (object obj2 in base.GetType().GetCustomAttributes(false))
            {
                if (obj2 is FormAttribute)
                {
                    attribute = obj2 as FormAttribute;

                    if (!string.IsNullOrEmpty(attribute.formUid))
                    {
                        string form = attribute.formUid;
                        this.formUid.Add(form.Substring(0, 1) == "@" ? form.Replace("@", "UDO_FT_") : form);

                    }

                    string formulario = this.formUid.SingleOrDefault(e => e == attribute.formUid);

                    if (!string.IsNullOrEmpty(formulario))
                    {
                        B1AppDomain.RegisterFormByType(formulario, this);
                        
                    }

                    

                    index++;

                }
            }
            if (attribute == null)
            {
                B1Exception.writeLog("Falha ao indexar Form. Por favor checar os atributos informados");
            }
            
            
            this.OnInitializeFormEvents();


        }



        private void OnInitializeFormEvents()
        {

            #region Item Listeners
            Global.ItemPressed_Before += new Global.ItemEventHandler(ItemPressedFilterBefore);
            Global.ItemPressed_After += new Global.ItemEventHandler(ItemPressedFilterAfter);

            Global.ItemClicked_Before += new Global.ItemEventHandler(ItemClickedFilterBefore);
            Global.ItemClicked_After += new Global.ItemEventHandler(ItemClickedFilterAfter);
            Global.ChooseFromList_Before += new Global.ItemEventHandler(ChooseFromListFilterBefore);
            Global.ChooseFromList_After += new Global.ItemEventHandler(ChooseFromListFilterAfter);
            Global.ComboSelect_Before += new Global.ItemEventHandler(ComboSelectFilterBefore);
            Global.ComboSelect_After += new Global.ItemEventHandler(ComboSelectFilterAfter);

            Global.Form_Unload_Before += new Global.ItemEventHandler(FormUnloadFilterBefore);
            Global.Form_Unload_After += new Global.ItemEventHandler(FormUnloadFilterAfter);

            Global.DoubleClick_Before += new Global.ItemEventHandler(DoubleClickFilterBefore);
            Global.DoubleClick_After += new Global.ItemEventHandler(DoubleClickFilterAfter);
            Global.FormActivate_Before += new Global.ItemEventHandler(FormActivateFilterBefore);
            Global.FormActivate_After += new Global.ItemEventHandler(FormActivateFilterAfter);
            Global.FormClose_Before += new Global.ItemEventHandler(FormCloseFilterBefore);
            Global.FormClose_After += new Global.ItemEventHandler(FormCloseFilterAfter);
            Global.FormKeyDown_Before += new Global.ItemEventHandler(FormKeyDownFilterBefore);
            Global.FormKeyDown_After += new Global.ItemEventHandler(FormKeyDownFilterAfter);

            Global.KeyDown_Before += new Global.ItemEventHandler(KeyDownFilterBefore);
            Global.KeyDown_After += new Global.ItemEventHandler(KeyDownFilterAfter);

            Global.FormDeactivate_Before += new Global.ItemEventHandler(FormDeactivateFilterBefore);
            Global.FormDeactivate_After += new Global.ItemEventHandler(FormDeactivateFilterAfter);
            Global.GotFocus_Before += new Global.ItemEventHandler(GotFocusFilterBefore);
            Global.GotFocus_After += new Global.ItemEventHandler(GotFocusFilterAfter);
            Global.LostFocus_Before += new Global.ItemEventHandler(LostFocusFilterBefore);
            Global.LostFocus_After += new Global.ItemEventHandler(LostFocusFilterAfter);
            Global.Form_Load_Before += new Global.ItemEventHandler(FormLoadFilterBefore);
            Global.Form_Load_After += new Global.ItemEventHandler(FormLoadFilterAfter);
            Global.Matrix_Link_Pressed_Before += new Global.ItemEventHandler(MatrixLinkPressedFilterBefore);
            Global.Matrix_Link_Pressed_After += new Global.ItemEventHandler(MatrixLinkPressedFilterAfter);
            Global.Form_Resize_Before += new Global.ItemEventHandler(FormResizeFilterBefore);
            Global.Form_Resize_After += new Global.ItemEventHandler(FormResizeFilterAfter);

            #endregion

            #region RightClick Listeners
            Global.RightClick_Before += new Global.RightClickEventHandler(RightClickFilterBefore);
            Global.RightClick_After += new Global.RightClickEventHandler(RightClickFilterAfter);
            #endregion

            #region FormData Listeners
            Global.FormDataAdd_Before += new Global.FormDataEventHandler(FormDataAddFilterBefore);
            Global.FormDataAdd_After += new Global.FormDataEventHandler(FormDataAddFilterAfter);
            Global.FormDataDelete_Before += new Global.FormDataEventHandler(FormDataDeleteFilterBefore);
            Global.FormDataDelete_After += new Global.FormDataEventHandler(FormDataDeleteFilterAfter);
            Global.FormDataLoad_Before += new Global.FormDataEventHandler(FormDataLoadFilterBefore);
            Global.FormDataLoad_After += new Global.FormDataEventHandler(FormDataLoadFilterAfter);
            Global.FormDataUpdate_Before += new Global.FormDataEventHandler(FormDataUpdateFilterBefore);
            Global.FormDataUpdate_After += new Global.FormDataEventHandler(FormDataUpdateFilterAfter);
            #endregion
            
            #region ProgressBar Listeners
            Global.ProgressBar_Created_After += new Global.ProgressBarEventHandler(ProgressBar_Created_After);
            Global.ProgressBar_Created_Before += new Global.ProgressBarEventHandler(ProgressBar_Created_Before);
            Global.ProgressBar_Released_After += new Global.ProgressBarEventHandler(ProgressBar_Released_After);
            Global.ProgressBar_Released_Before += new Global.ProgressBarEventHandler(ProgressBar_Released_Before);
            Global.ProgressBar_Stopped_After += new Global.ProgressBarEventHandler(ProgressBar_Stopped_After);
            Global.ProgressBar_Stopped_Before += new Global.ProgressBarEventHandler(ProgressBar_Stopped_Before);
            #endregion

            #region UDO Listeners
            Global.UdoFormOpen += new Global.UdoEventHandler(UdoFormOpen);
            Global.UdoFormBuild += new Global.UdoEventHandler(UdoFormBuild);
            #endregion

            #region App Listeners
            Global.AppCompanyChanged += new Global.AppEventHandler(AppCompanyChanged);
            Global.AppLanguageChanged += new Global.AppEventHandler(AppLanguageChanged);
            Global.AppFontChanged += new Global.AppEventHandler(AppFontChanged);
            Global.AppServerTerminition += new Global.AppEventHandler(AppServerTerminition);
            Global.AppShutdown += new Global.AppEventHandler(AppShutdown);
            #endregion

            #region Print Listeners

            Global.Print_Before += new Global.PrintEventHandler(PrintFilterBefore);
            Global.Print_After += new Global.PrintEventHandler(PrintFilterAfter);
            Global.Print_Data_Before += new Global.ReportDataEventHandler(PrintDataFilterBefore);
            Global.Print_Data_After += new Global.ReportDataEventHandler(PrintDataFilterAfter);
            Global.Print_Layout_Before += new Global.PrintEventHandler(PrintLayoutFilterBefore);
            Global.Print_Layout_After += new Global.PrintEventHandler(PrintLayoutFilterAfter);

            #endregion


        }

        
        
        #region ProgressBar Actions
        public virtual void ProgressBar_Stopped_Before(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }
        public virtual void ProgressBar_Stopped_After(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }

        public virtual void ProgressBar_Released_Before(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }
        public virtual void ProgressBar_Released_After(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }

        public virtual void ProgressBar_Created_Before(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }
        public virtual void ProgressBar_Created_After(ref ProgressBarEvent pVal, ref bool bubbleEvent) { }
        #endregion

        #region Item Actions


        private void FormUnloadFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                Form_Unload_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormUnloadFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                Form_Unload_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void Form_Unload_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void Form_Unload_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void ItemPressedFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.Item(formUID);
                ItemPressed_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void ItemPressedFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.Item(formUID);
                ItemPressed_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void ItemPressed_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void ItemPressed_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void ItemClickedFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.Item(formUID);
                ItemClicked_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void ItemClickedFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.Item(formUID);
                ItemClicked_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void ItemClicked_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void ItemClicked_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void ChooseFromListFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                ChooseFromList_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void ChooseFromListFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                ChooseFromList_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void ChooseFromList_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void ChooseFromList_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void ComboSelectFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                ComboSelect_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void ComboSelectFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                ComboSelect_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void ComboSelect_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void ComboSelect_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void DoubleClickFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.Item(formUID);
                DoubleClick_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void DoubleClickFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.Item(formUID);
                DoubleClick_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void DoubleClick_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void DoubleClick_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormActivateFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormActivate_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormActivateFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormActivate_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void FormActivate_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void FormActivate_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormCloseFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormClose_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormCloseFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormClose_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void FormClose_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void FormClose_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormKeyDownFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormKeyDown_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormKeyDownFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormKeyDown_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void FormKeyDown_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void FormKeyDown_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }


        private void KeyDownFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                KeyDown_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void KeyDownFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                KeyDown_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void KeyDown_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void KeyDown_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }


        private void FormDeactivateFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormDeactivate_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormDeactivateFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                FormDeactivate_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void FormDeactivate_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void FormDeactivate_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void GotFocusFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                GotFocus_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void GotFocusFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                GotFocus_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void GotFocus_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void GotFocus_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void LostFocusFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                LostFocus_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void LostFocusFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                LostFocus_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void LostFocus_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void LostFocus_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormLoadFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.Item(formUID);
                Form_Load_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormLoadFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.Item(formUID);
                Form_Load_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void Form_Load_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void Form_Load_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void MatrixLinkPressedFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                Matrix_Link_Pressed_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void MatrixLinkPressedFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                Matrix_Link_Pressed_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void Matrix_Link_Pressed_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void Matrix_Link_Pressed_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }

        private void FormResizeFilterAfter(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                Form_Resize_After(formUID, ref pVal, ref bubbleEvent);
            }
        }
        private void FormResizeFilterBefore(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                Form_Resize_Before(formUID, ref pVal, ref bubbleEvent);
            }
        }
        public virtual void Form_Resize_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        public virtual void Form_Resize_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent) { }
        
        #endregion

        #region App Actions
        public virtual void AppCompanyChanged(ref BoAppEventTypes pVal) { }
        public virtual void AppLanguageChanged(ref BoAppEventTypes pVal) { }
        public virtual void AppFontChanged(ref BoAppEventTypes pVal) { }
        public virtual void AppServerTerminition(ref BoAppEventTypes pVal) { }
        public virtual void AppShutdown(ref BoAppEventTypes pVal) { }
        #endregion

        #region UDO Actions
        public virtual void UdoFormBuild(ref UDOEvent pVal, ref bool bubbleevent) { }
        private void UdoForm_Open(ref UDOEvent pVal, ref bool bubbleevent) {
            if (this.formUid.Contains(pVal.ObjectKey))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                UdoFormOpen(ref pVal, ref bubbleevent);
            }
        }
        public virtual void UdoFormOpen(ref UDOEvent pVal, ref bool bubbleevent) { }
        #endregion

        #region FormData Actions

        private void FormDataAddFilterBefore(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                FormDataAdd_Before(ref pVal, ref bubbleevent);
            }
        }
        private void FormDataAddFilterAfter(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                FormDataAdd_After(ref pVal, ref bubbleevent);
            }
        }
        public virtual void FormDataAdd_Before(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        public virtual void FormDataAdd_After(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }


        private void FormDataDeleteFilterBefore(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                FormDataDelete_Before(ref pVal, ref bubbleevent);
            }
        }
        private void FormDataDeleteFilterAfter(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                FormDataDelete_After(ref pVal, ref bubbleevent);
            }
        }
        public virtual void FormDataDelete_Before(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        public virtual void FormDataDelete_After(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }


        private void FormDataUpdateFilterBefore(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                FormDataUpdate_Before(ref pVal, ref bubbleevent);
            }
        }
        private void FormDataUpdateFilterAfter(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                FormDataUpdate_After(ref pVal, ref bubbleevent);
            }
        }
        public virtual void FormDataUpdate_Before(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        public virtual void FormDataUpdate_After(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }



        private void FormDataLoadFilterBefore(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                FormDataLoad_Before(ref pVal, ref bubbleevent);
            }
        }
        private void FormDataLoadFilterAfter(ref BusinessObjectInfo pVal, ref bool bubbleevent)
        {
            if (this.formUid.Contains(pVal.FormTypeEx))
            {
                oForm = B1AppDomain.Application.Forms.ActiveForm;
                FormDataLoad_After(ref pVal, ref bubbleevent);
            }
        }
        public virtual void FormDataLoad_Before(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        public virtual void FormDataLoad_After(ref BusinessObjectInfo pVal, ref bool bubbleevent) { }
        #endregion

        #region RightClick Actions
        public virtual void RightClickFilterAfter(ref ContextMenuInfo pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormUID))
            {
                RightClick_After(ref pVal, ref bubbleEvent);
            }
        }
        public virtual void RightClickFilterBefore(ref ContextMenuInfo pVal, ref bool bubbleEvent)
        {
            if (this.formUid.Contains(pVal.FormUID))
            {
                RightClick_Before(ref pVal, ref bubbleEvent);
            }
        }
        public virtual void RightClick_After(ref ContextMenuInfo pVal, ref bool bubbleEvent) { }
        public virtual void RightClick_Before(ref ContextMenuInfo pVal, ref bool bubbleEvent) { }
        #endregion
        
        #region Print Actions

        private void PrintFilterBefore(ref PrintEventInfo pVal, ref bool bubbleEvent)
        {
            
                Print_Before(ref pVal, ref bubbleEvent);
            
        }

        private void PrintFilterAfter(ref PrintEventInfo pVal, ref bool bubbleEvent)
        {
            
                Print_After(ref pVal, ref bubbleEvent);
            
        }

        public virtual void Print_Before(ref PrintEventInfo pVal, ref bool bubbleEvent) { }
        public virtual void Print_After(ref PrintEventInfo pVal, ref bool bubbleEvent) { }


        private void PrintDataFilterBefore(ref ReportDataInfo pVal, ref bool bubbleEvent)
        {
            
                Print_Data_Before(ref pVal, ref bubbleEvent);
            
        }

        private void PrintDataFilterAfter(ref ReportDataInfo pVal, ref bool bubbleEvent)
        {
            
                Print_Data_After(ref pVal, ref bubbleEvent);
            
        }

        public virtual void Print_Data_Before(ref ReportDataInfo pVal, ref bool bubbleEvent) { }
        public virtual void Print_Data_After(ref ReportDataInfo pVal, ref bool bubbleEvent) { }


        private void PrintLayoutFilterBefore(ref PrintEventInfo pVal, ref bool bubbleEvent)
        {
            
                Print_Layout_Before(ref pVal, ref bubbleEvent);
            
        }

        private void PrintLayoutFilterAfter(ref PrintEventInfo pVal, ref bool bubbleEvent)
        {
            
                Print_Layout_After(ref pVal, ref bubbleEvent);
            
        }

        public virtual void Print_Layout_Before(ref PrintEventInfo pVal, ref bool bubbleEvent) { }
        public virtual void Print_Layout_After(ref PrintEventInfo pVal, ref bool bubbleEvent) { }

        #endregion

    }
}
