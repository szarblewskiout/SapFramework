using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SapFramework.Connections;
using SAPbobsCOM;

namespace SapFramework.BaseDados.Sistema
{
    public class UFD1
    {

        public string TableID { get; set; }
        public int FieldID { get; set; }
        public int IndexID { get; set; }
        public string FldValue { get; set; }
        public string Descr { get; set; }
        public DateTime FldDate { get; set; }



        internal static List<UFD1> RetornaValores()
        {

            List<UFD1> lista =new List<UFD1>();

            Recordset oRs = B1AppDomain.RSQuery(@"SELECT [TableID]
      ,[FieldID]
      ,[IndexID]
      ,[FldValue]
      ,[Descr]
      ,[FldDate]
  FROM [UFD1]");


            while (!oRs.EoF)
            {
                
                lista.Add(new UFD1()
                {
                    TableID = oRs.Fields.Item("TableID").Value.ToString(),
                    FieldID = oRs.Fields.Item("FieldID").Value.ToString(),
                    IndexID = oRs.Fields.Item("IndexID").Value.ToString(),
                    FldValue = oRs.Fields.Item("FldValue").Value.ToString(),
                    Descr = oRs.Fields.Item("Descr").Value.ToString(),
                    FldDate = oRs.Fields.Item("FldDate").Value.ToString()
                });

                oRs.MoveNext();
            }


            Objects.LimpaMemoria(oRs);

            return lista;

        }

        internal static List<UFD1> RetornaValores(string nomeTabela, string nomeCampo)
        {

            List<UFD1> lista = new List<UFD1>();

            Recordset oRs = B1AppDomain.RSQuery(string.Format(@"SELECT [TableID]
      ,[FieldID]
      ,[IndexID]
      ,[FldValue]
      ,[Descr]
      ,[FldDate]
  FROM [UFD1] where tableid = '{0}' and fieldid = (select FieldID from CUFD where TableID = '{0}' and AliasID = '{1}')", nomeTabela, nomeCampo));


            while (!oRs.EoF)
            {

                lista.Add(new UFD1()
                {
                    TableID = oRs.Fields.Item("TableID").Value.ToString(),
                    FieldID = Convert.ToInt32(oRs.Fields.Item("FieldID").Value.ToString()),
                    IndexID = Convert.ToInt32(oRs.Fields.Item("IndexID").Value.ToString()),
                    FldValue = oRs.Fields.Item("FldValue").Value.ToString(),
                    Descr = oRs.Fields.Item("Descr").Value.ToString(),
                    FldDate = Convert.ToDateTime(oRs.Fields.Item("FldDate").Value.ToString())
                });

                oRs.MoveNext();
            }

            Objects.LimpaMemoria(oRs);


            return lista;

        }

    }
}
