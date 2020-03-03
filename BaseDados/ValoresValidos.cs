using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapFramework.BaseDados
{
    public class ValoresValidos : IValoresValidos
    {


        public void Add()
        {
            new ValoresValidos();
        }

        public string Valor { get; set; }
        public string Descricao { get; set; }
    }
}
