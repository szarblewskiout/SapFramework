using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.BaseDados.Sistema
{
    public class CUFD
    {

        public string TableID { get; set; }
        public int FieldID { get; set; }
        public string AliasID { get; set; }
        public string Descr { get; set; }
        public string TypeID { get; set; }
        public string EditType { get; set; }
        public int SizeID { get; set; }
        public int EditSize { get; set; }
        public string Dflt { get; set; }
        public string NotNull { get; set; }
        public string IndexID { get; set; }
        public string RTable { get; set; }
        public int RField { get; set; }
        public string Action { get; set; }
        public string Sys { get; set; }
        public DateTime DfltDate { get; set; }
        public string RelUDO { get; set; }



        internal static List<CUFD> RetornaValores()
        {

            var lista = new List<CUFD>();

            SAPbobsCOM.Recordset oRs = SapFramework.Connections.B1AppDomain.RSQuery(@"SELECT TableID
      , FieldID
      , AliasID
      , Descr
      , TypeID
      , EditType
      , SizeID
      , EditSize
      , Dflt
      , NotNull
      , IndexID
      , RTable
      , RField
      , Action
      , Sys
      , DfltDate
      , RelUDO
  FROM CUFD");

            while (!oRs.EoF)
            {
                lista.Add(new CUFD()
                {
                    TableID = oRs.Fields.Item("TableID").Value.ToString(),
                    FieldID = Convert.ToInt32(oRs.Fields.Item("FieldID").Value.ToString()),
                    AliasID = oRs.Fields.Item("AliasID").Value.ToString(),
                    Descr = oRs.Fields.Item("Descr").Value.ToString(),
                    TypeID = oRs.Fields.Item("TypeID").Value.ToString(),
                    EditType = oRs.Fields.Item("EditType").Value.ToString(),
                    SizeID = Convert.ToInt32(oRs.Fields.Item("SizeID").Value.ToString()),
                    EditSize = Convert.ToInt32(oRs.Fields.Item("EditSize").Value.ToString()),
                    Dflt = oRs.Fields.Item("Dflt").Value.ToString(),
                    NotNull = oRs.Fields.Item("NotNull").Value.ToString(),
                    IndexID = oRs.Fields.Item("IndexID").Value.ToString(),
                    RTable = oRs.Fields.Item("RTable").Value.ToString(),
                    RField = Convert.ToInt32(oRs.Fields.Item("RField").Value.ToString()),
                    Action = oRs.Fields.Item("Action").Value.ToString(),
                    Sys = oRs.Fields.Item("Sys").Value.ToString(),
                    DfltDate = Convert.ToDateTime(oRs.Fields.Item("DfltDate").Value.ToString()),
                    RelUDO = oRs.Fields.Item("RelUDO").Value.ToString()
                });

                oRs.MoveNext();
            }


            Objects.LimpaMemoria(oRs);

            return lista;


        }

        internal static List<CUFD> RetornaValores(string nomeTabela)
        {

            var lista = new List<CUFD>();

            SAPbobsCOM.Recordset oRs = SapFramework.Connections.B1AppDomain.RSQuery(string.Format(@"SELECT TableID
      , FieldID
      , AliasID
      , Descr
      , TypeID
      , EditType
      , SizeID
      , EditSize
      , Dflt
      , NotNull
      , IndexID
      , RTable
      , RField
      , Action
      , Sys
      , DfltDate
      , RelUDO
  FROM CUFD where tableid = '{0}'", nomeTabela));

            while (!oRs.EoF)
            {
                lista.Add(new CUFD()
                {
                    TableID = oRs.Fields.Item("TableID").Value.ToString(),
                    FieldID = Convert.ToInt32(oRs.Fields.Item("FieldID").Value.ToString()),
                    AliasID = oRs.Fields.Item("AliasID").Value.ToString(),
                    Descr = oRs.Fields.Item("Descr").Value.ToString(),
                    TypeID = oRs.Fields.Item("TypeID").Value.ToString(),
                    EditType = oRs.Fields.Item("EditType").Value.ToString(),
                    SizeID = Convert.ToInt32(oRs.Fields.Item("SizeID").Value.ToString()),
                    EditSize = Convert.ToInt32(oRs.Fields.Item("EditSize").Value.ToString()),
                    Dflt = oRs.Fields.Item("Dflt").Value.ToString(),
                    NotNull = oRs.Fields.Item("NotNull").Value.ToString(),
                    IndexID = oRs.Fields.Item("IndexID").Value.ToString(),
                    RTable = oRs.Fields.Item("RTable").Value.ToString(),
                    RField = Convert.ToInt32(oRs.Fields.Item("RField").Value.ToString()),
                    Action = oRs.Fields.Item("Action").Value.ToString(),
                    Sys = oRs.Fields.Item("Sys").Value.ToString(),
                    DfltDate = Convert.ToDateTime(oRs.Fields.Item("DfltDate").Value.ToString()),
                    RelUDO = oRs.Fields.Item("RelUDO").Value.ToString()
                });

                oRs.MoveNext();
            }


            Objects.LimpaMemoria(oRs);

            return lista;


        }

    }
}
