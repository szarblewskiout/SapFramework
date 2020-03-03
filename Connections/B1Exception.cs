using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapFramework.Connections
{
    public sealed class B1Exception
    {

        static private B1Exception objException = null;

        private B1Exception()
        {
        }

        static public void throwException(string strAssembly, Exception er)
        {

            string strErrorMessage = "";

            if (objException == null)

                objException = new B1Exception();

            try
            {

                if (er != null && er.Message.Length > 0)
                {
                    System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(er, true);
                    string strGetAssemblyInfo = "Method:" + trace.GetFrame(0).GetMethod().Name + " Line: " + trace.GetFrame(0).GetFileLineNumber() + " Column: " + trace.GetFrame(0).GetFileColumnNumber();
                    strErrorMessage = strAssembly + "-" + strGetAssemblyInfo + " ::: " + er.Message;
                }
                else
                {
                    strErrorMessage = strAssembly;
                }

                writeLog(strErrorMessage);








            }

            catch (Exception objEx)
            {



                writeLog("ExceptionClass:" + strAssembly + ": " + objEx.Message);



            }



        }

        static public void throwException(int codErrorSap)
        {
            string strErrorMessage = "";

            if (objException == null)

                objException = new B1Exception();

            try
            {

                if (codErrorSap != 0)
                {
                    string mensagem = "";
                    B1AppDomain.Company.GetLastError(out codErrorSap, out mensagem);
                    strErrorMessage = codErrorSap + " :: " + mensagem;

                    writeLog(strErrorMessage);
                }


            }
            catch (Exception objEx)
            {

                writeLog("ExceptionClass:" + ": " + objEx.Message);
            }



        }

        static public void writeLog(string strMessage)
        {

            string strDate = DateTime.Now.ToString("yyyyMMdd");

            string strPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + Settings.ConfigApplication.Default.LogPath + "\\";

            try
            {
                B1AppDomain.Application.SetStatusBarMessage(strMessage);

                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }


                using (StreamWriter sw = new StreamWriter(strPath + "\\" + Settings.ConfigApplication.Default.LogName + "-" + strDate + ".log", true))
                {
                    sw.WriteLine(DateTime.Now.ToLongTimeString());
                    sw.WriteLine(strMessage);
                }





            }

            catch (Exception er)
            {

                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(er, true);

                string strGetAssemblyInfo = trace.GetFrame(0).GetMethod().Name + " Line: " + trace.GetFrame(0).GetFileLineNumber() + " Column: " + trace.GetFrame(0).GetFileColumnNumber();

                string strErrorMessage = er.Message + " ::: " + strGetAssemblyInfo;

                MessageBox.Show(strErrorMessage);

            }

        }



    }
}
