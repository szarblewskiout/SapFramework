using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapFramework.BaseDados
{
    public interface IValoresValidos
    {

        void Add();

        string Valor { set; }
        string Descricao { set; }



    }
}
