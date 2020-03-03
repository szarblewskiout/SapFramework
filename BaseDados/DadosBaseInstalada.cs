using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework.BaseDados
{
    public class TabelasBaseInstalada
    {

        public string TableName { get; set; }
        public string Descr { get; set; }
        public int TblNum { get; set; }
        public string ObjectType { get; set; }
        public string UsedInObj { get; set; }
        public string LogTable { get; set; }
        public string Archivable { get; set; }
        public string ArchivDate { get; set; }



    }

    public class CamposBaseInstalada
    {
        public string NomeTabela { get; set; }
        public string NomeCampo { get; set; }
    }


}
