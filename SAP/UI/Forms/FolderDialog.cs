using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SapFramework.Connections;
using SAPbouiCOM;

namespace SapFramework.SAP.UI.Forms
{
    public static class FolderDialog
    {
        private static string Caminho = "";
        private static string Mensagem = "";

        private static void BuscaCaminhoXML()
        {
            WindowWrapper owner = new WindowWrapper();
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = Mensagem,
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false
            };
            dialog.ShowDialog(owner);
            Caminho = dialog.SelectedPath;
        }

        public static void SelecionaCaminho(SAPbouiCOM.Form oForm, string mensagem, string nomeCampo)
        {
            Mensagem = mensagem;
            Thread thread = null;
            try
            {
                thread = new Thread(new ThreadStart(FolderDialog.BuscaCaminhoXML));
                if (thread.ThreadState == ThreadState.Unstarted)
                {
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else if (thread.ThreadState == ThreadState.Stopped)
                {
                    thread.Start();
                    thread.Join();
                }
                while (thread.ThreadState == ThreadState.Running)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                ((EditText)oForm.Items.Item(nomeCampo).Specific).Value = Caminho;
            }
            catch (Exception exception)
            {
                B1Exception.throwException("QBC.Forms.FolderDialog.SelecionaCaminho ::", exception);
            }
        }
    }
}
