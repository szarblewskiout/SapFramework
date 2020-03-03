using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.BaseDados.Sistema
{
    public class OUTB
    {

        public string TableName { get; set; }
        public string Descr { get; set; }
        public int TblNum { get; set; }
        public string ObjectType { get; set; }
        public string UsedInObj { get; set; }
        public string LogTable { get; set; }
        public string Archivable { get; set; }
        public string ArchivDate { get; set; }


        internal static List<OUTB> RetornaValores()
        {

            var lista = new List<OUTB>();

            SAPbobsCOM.Recordset oRs = SapFramework.Connections.B1AppDomain.RSQuery("select TableName, Descr, TblNum, ObjectType, UsedInObj, LogTable, Archivable, ArchivDate from OUTB");

            while (!oRs.EoF)
            {
                lista.Add(new OUTB()
                {
                    TableName = oRs.Fields.Item("TableName").Value.ToString(),
                    Descr = oRs.Fields.Item("Descr").Value.ToString(),
                    TblNum = Convert.ToInt32(oRs.Fields.Item("TblNum").Value.ToString()),
                    ObjectType = oRs.Fields.Item("ObjectType").Value.ToString(),
                    Archivable = oRs.Fields.Item("Archivable").Value.ToString(),
                    ArchivDate = oRs.Fields.Item("ArchivDate").Value.ToString(),
                    LogTable = oRs.Fields.Item("LogTable").Value.ToString(),
                    UsedInObj = oRs.Fields.Item("UsedInObj").Value.ToString()
                });

                oRs.MoveNext();

            }


            Objects.LimpaMemoria(oRs);

            return lista;


        } 



    }
}
