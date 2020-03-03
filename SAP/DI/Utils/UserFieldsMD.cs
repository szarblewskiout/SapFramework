using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SapFramework.BaseDados;

namespace SapFramework.SAP.DI.Utils
{
    public static class UserFieldsMDClass
    {

        public static SAPbobsCOM.UserFieldsMD AddValidValues(this SAPbobsCOM.UserFieldsMD oUserFieldsMd, List<SapFramework.BaseDados.ValoresValidos> valores)
        {
            if (valores != null)
            {
                int volta = 1;

                foreach (ValoresValidos valor in valores)
                {

                    oUserFieldsMd.ValidValues.Value = valor.Valor;
                    oUserFieldsMd.ValidValues.Description = valor.Descricao;
                    if (volta <= valores.Count)
                    {
                        oUserFieldsMd.ValidValues.Add();
                    }
                    volta++;
                }
            }
            return oUserFieldsMd;
        }


    }
}
